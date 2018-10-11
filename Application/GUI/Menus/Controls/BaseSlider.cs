using LudicrousElectron.Engine.Input;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using OpenTK;
using System;

namespace LunarLambda.GUI.Menus.Controls
{
    public abstract class BaseSlider : UIPanel
	{
		protected UIButton AdvanceButton = null;
		protected UIButton RetreatButton = null;
		protected UIButton ThumbButton = null;

		public int MaxValue = 100;
		public int MinValue = 0;

		public int ValueStep = 10;

		public int CurrentValue { get; protected set; } = 0;
		public event EventHandler<BaseSlider> ValueChanged = null;

		protected bool Vertical = true;
		public bool UseWheelInput = true;

		protected BaseSlider(RelativeRect rect, int value, int min = 0, int max = 100, string texture = null) : base(rect, texture)
		{
			IgnoreMouse = false;
			FillMode = UIFillModes.SmartStprite;
			MinValue = min;
			MaxValue = max;
			CurrentValue = value;
			SetupButtons();

			if (RetreatButton != null)
				AddChild(RetreatButton);

			if (AdvanceButton != null)
				AddChild(AdvanceButton);

			if (ThumbButton != null)
				AddChild(ThumbButton);

			if (AdvanceButton != null)
				AdvanceButton.Clicked += AdvanceButton_Clicked;

			if (RetreatButton != null)
				RetreatButton.Clicked += RetreatButton_Clicked;


			if (AdvanceButton != null && CurrentValue == MaxValue)
				AdvanceButton.Disable();
			else if (RetreatButton != null && CurrentValue == MinValue)
				RetreatButton.Disable();
		}

		protected abstract void SetupButtons();

		protected virtual double CurrentParam()
		{
			if (MaxValue == MinValue)
				return 0;

			return (double)(CurrentValue - MinValue) / (double)MaxValue - MinValue;
		}

		public virtual void ResetScroll(bool refresh = false)
		{
			CurrentValue = MinValue;
			if (refresh)
				ForceRefresh();
		}

		private void AdvanceButton_Clicked(object sender, UIButton e)
		{
			Advance(1);
		}

		public virtual void Advance(int count)
		{
			CurrentValue += ValueStep * count;
			if (CurrentValue > MaxValue)
				CurrentValue = MaxValue;
			else
			{
				if (CurrentValue == MaxValue)
					AdvanceButton.Disable();
				else
					AdvanceButton.Enable();
				RetreatButton.Enable();
				SetThumbPos(true);
				ForceRefresh();
				ValueChanged?.Invoke(this, this);
			}
		}

		private void RetreatButton_Clicked(object sender, UIButton e)
		{
			Retreat(1);
		}
		public virtual void Retreat(int count)
		{
			CurrentValue -= ValueStep * count;
			if (CurrentValue < MinValue)
				CurrentValue = MinValue;
			else
			{
				if (CurrentValue == MinValue)
					RetreatButton.Disable();
				else
					RetreatButton.Enable();
				AdvanceButton.Enable();
				SetThumbPos(true);
				ForceRefresh();

				ValueChanged?.Invoke(this, this);
			}
		}

		public override void Resize(int x, int y)
		{
			// see how big this is in pixel space so we can position the thumb
			this.Rect.Resize(x, y);
			SetThumbPos(false);

			base.Resize(x, y);
		}

		protected virtual void SetThumbPos(bool forceThumbResize)
		{
			float paramDist = GetParamDistance();
			if (Vertical)
			{
				ThumbButton.Rect.Y.Paramater = GetThumbStart() - paramDist;
				ThumbButton.Rect.Y.RelativeTo = RelativeLoc.Edge.Raw;
			}
			else
			{
				ThumbButton.Rect.X.Paramater = GetThumbStart() + paramDist;
				ThumbButton.Rect.X.RelativeTo = RelativeLoc.Edge.Raw;
			}

			if (forceThumbResize)
				ThumbButton.ForceRefresh();
		}

		protected float GetAvalailableSliderDistance()
		{
			if (Vertical)
				return Rect.GetPixelSize().Y - GetButtonDistance();
			else
				return Rect.GetPixelSize().X - GetButtonDistance();
		}

		protected float GetAvalailableClickDistance()
		{
			float width = Rect.GetPixelSize().X;
			float height = Rect.GetPixelSize().Y;

			if (Vertical)
			{
				float vbuttons = 0;
				if (AdvanceButton != null)
					vbuttons += width * 0.5f;
				if (RetreatButton != null)
					vbuttons += width * 0.5f;

				return height - vbuttons;
			}
			else
			{
				float hbuttons = 0;
				if (AdvanceButton != null)
					hbuttons += height * 0.5f;
				if (RetreatButton != null)
					hbuttons += height * 0.5f;

				return width - hbuttons;
			}
		}

		protected float GetParamDistance()
		{
			return GetAvalailableSliderDistance() * (float)(CurrentParam());
		}

		protected float GetButtonDistance()
		{
			float width = Rect.GetPixelSize().X;
			float height = Rect.GetPixelSize().Y;

			if (Vertical)
			{
				float vbuttons = 0;
				if (AdvanceButton != null)
					vbuttons += width;
				if (RetreatButton != null)
					vbuttons += width;

				return vbuttons;
			}
			else
			{
				float hbuttons = 0;
				if (AdvanceButton != null)
					hbuttons += height;
				if (RetreatButton != null)
					hbuttons += height;

				return hbuttons;
			}
		}

		protected float GetThumbStart()
		{
			if (Vertical)
				return Rect.GetPixelOrigin().Y + (Rect.GetPixelSize().Y - (RetreatButton != null ? Rect.GetPixelSize().X : 0));
			else
				return Rect.GetPixelOrigin().X + (RetreatButton != null ? Rect.GetPixelSize().Y : 0);
		}

		protected float GetClickAreaStart()
		{
			if (Vertical)
				return Rect.GetPixelOrigin().Y + (Rect.GetPixelSize().Y - (RetreatButton != null ? Rect.GetPixelSize().X * 0.5f : 0));
			else
				return Rect.GetPixelOrigin().X + (RetreatButton != null ? Rect.GetPixelSize().Y * 0.5f : 0);
		}

		public override void ProcessMouseEvent(Vector2 location, InputManager.LogicalButtonState buttons)
		{
			int wheelAbs = Math.Abs(buttons.WheelTick);
			if (UseWheelInput && wheelAbs > 0)
			{
				bool advance = buttons.WheelTick > 0;
				if (Vertical)
					advance = !advance;

				if (!advance)
					Retreat(Math.Abs(wheelAbs));
				else
					Advance(Math.Abs(wheelAbs));
			}

			if (!buttons.PrimaryDown)
				return;

			float availableDist = GetAvalailableClickDistance();
			if (availableDist <= 0)
				return;

			float param = 0;

			float areaStart = GetClickAreaStart();
			float areaEnd = areaStart + (Vertical ? -availableDist : availableDist);

			if (Vertical)
			{
				if (location.Y < areaEnd || location.Y > areaStart)		// Y has reversed axis dir from the mouse
					return;
				param = (areaStart - location.Y) / availableDist;
			}
			else
			{
				if (location.X < areaStart || location.X > areaEnd)
					return;
				param = (location.X - areaStart) / availableDist;
			}

			int range = MaxValue - MinValue;
			int newVal = (int)((param * range) + MinValue + 0.25f);

			if (newVal == CurrentValue)
				return;

			CurrentValue = newVal;

			ValueChanged?.Invoke(this, this);

			if (CurrentValue == MinValue)
				RetreatButton.Disable();
			else
				RetreatButton.Enable();

			if (CurrentValue == MaxValue)
				AdvanceButton.Disable();
			else
				AdvanceButton.Enable();

			SetThumbPos(true);
		}
	}
}
