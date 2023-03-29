using Components;

namespace Behaviours
{
    public class BallisticProjectileBehaviourContainer : MoveBehaviourContainer
    {
        public BallisticProjectileMoveBehaviour ballisticProjectileMoveBehaviour;
        public override IMoveBehariour GetValue => ballisticProjectileMoveBehaviour;

        protected override void Init()
        {
            if (TryGetComponent<IBallisticProjectile>(out var projectile))
                ballisticProjectileMoveBehaviour = new BallisticProjectileMoveBehaviour(projectile);
        }

    }
}
