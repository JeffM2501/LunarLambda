using LudicrousElectron.Entities.Collisions;
using LunarLambda.Data.Databases;

namespace LunarLambda.Data.Entitites
{
    public class Ship : BaseEntity
    {
        public virtual ShipTemplate ShipInfo { get { return Template as ShipTemplate; } }

        public Ship(ShipTemplate template) : base()
        {
            SetTemplate(template);
        }

        public override void SetTemplate(BaseTemplate template)
        {
            if (template as ShipTemplate == null)
                return;
            Template = template;
            Name = template.Name;
        }

        public override void OnCollide(ICollisionable other)
        {
            // do some damage and movement stuff now
        }

        public override void OnUpdate(double dt, double now)
        {
            // tell the AI to update or something
        }
    }
}
