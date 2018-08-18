using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Speech.Synthesis;

using LunarLambda.API;
using LudicrousElectron.Engine.Audio;

namespace LunarLambda.Speach
{
    public class SpeachSupport : LLPlugin
	{
		public SpeechSynthesizer Synth = null;

		public override void Load()
		{
			base.Load();

			SoundManager.GetTextVoice += SoundManager_GetTextVoice;
			SoundManager.SetTextVoice += SoundManager_SetTextVoice;
			SoundManager.SpeakText += SoundManager_SpeakText;
			SoundManager.StopAllSpeach += SoundManager_StopAllSpeach;
			SoundManager.GetTextVoiceList += SoundManager_GetTextVoiceList;


			Synth = new SpeechSynthesizer();
			Synth.SetOutputToDefaultAudioDevice();

			string secondBestVoiceName = string.Empty;
			string bestVoiceName = string.Empty;
			foreach (var voice in (Synth as SpeechSynthesizer).GetInstalledVoices())
			{
				if (voice.VoiceInfo.Gender == VoiceGender.Female)
				{
					secondBestVoiceName = voice.VoiceInfo.Name;
					if (voice.VoiceInfo.Age == VoiceAge.Adult)
						bestVoiceName = voice.VoiceInfo.Name;
				}
			}

			if (bestVoiceName == string.Empty && secondBestVoiceName != string.Empty)
				bestVoiceName = secondBestVoiceName;

			SoundManager.SetTextToSpeachVoice(bestVoiceName);
		}

		private void SoundManager_GetTextVoiceList(object sender, SoundManager.SpeachEventArgs e)
		{
			List<string> prams = new List<string>(e.Text.Split(";".ToCharArray()));
			bool female = prams.Contains("female");
			bool male = prams.Contains("male");

			List<string> names = new List<string>();
			if (Synth == null || Synth as SpeechSynthesizer == null)
			{
				foreach (var voice in (Synth as SpeechSynthesizer).GetInstalledVoices())
				{
					switch (voice.VoiceInfo.Gender)
					{
						case VoiceGender.Female:
							if (female)
								names.Add(voice.VoiceInfo.Name);
							break;

						case VoiceGender.Male:
							if (male)
								names.Add(voice.VoiceInfo.Name);
							break;
						default:
							if (female == male) // they want them all or they want the neutral ones
								names.Add(voice.VoiceInfo.Name);
							break;
					}
				}

			}

			e.Text = string.Join(";", names.ToArray());
		}

		private void SoundManager_StopAllSpeach(object sender, SoundManager.SpeachEventArgs e)
		{
			(Synth as SpeechSynthesizer).SpeakAsyncCancelAll();
		}

		private void SoundManager_SpeakText(object sender, SoundManager.SpeachEventArgs e)
		{
			(Synth as SpeechSynthesizer).SpeakAsync(e.Text);
		}

		private void SoundManager_SetTextVoice(object sender, SoundManager.SpeachEventArgs e)
		{
			foreach (var voice in (Synth as SpeechSynthesizer).GetInstalledVoices())
			{
				if (voice.VoiceInfo.Name == e.Text)
				{
					(Synth as SpeechSynthesizer).SelectVoice(e.Text);
					return;
				}
			}
		}

		private void SoundManager_GetTextVoice(object sender, LudicrousElectron.Engine.Audio.SoundManager.SpeachEventArgs e)
		{
			e.Text = (Synth as SpeechSynthesizer).Voice.Name;
		}
	}
}
