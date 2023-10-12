namespace PatternTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IAnimal p = new Predator();
            IAnimal h = new Herbore();

            p.AcceptBehaviorPattern(new AngryPattern());
            h.AcceptBehaviorPattern(new PanicPattern());

            Console.WriteLine(new string('_', 10));

            p.AcceptBehaviorPattern(new PanicPattern());

            Console.WriteLine(new string('_', 10));

            Console.WriteLine("Здоровье хищника: " + p.Health);
            Console.WriteLine("Здоровье травоядного: " + h.Health);

            Console.WriteLine(new string('_', 10));
            
            Weapon standartKnife = new Knife(10);
            Weapon powerKnife = new Knife(100);

            p.ApplayDamage(standartKnife);
            h.ApplayDamage(powerKnife);

            Console.WriteLine(new string('_', 10));

            Console.WriteLine($"Хищнику нанесен урон\nЗдоровья осталось:{p.Health}\n");
            Console.WriteLine($"Травоядному нанесен урон\nЗдоровья осталось:{h.Health}");
        }
        public interface IBehaviorVisitor
        {
            public void VisitPredator(Predator e);
            public void VisitHerbore(Herbore e);
            public void VisitPredator(BehaviorElement e)
            {
                throw new NotImplementedException();
            }
            public void VisitHerbore(BehaviorElement e)
            {
                throw new NotImplementedException();
            }
        }


        public class AngryPattern : IBehaviorVisitor
        {
            public void VisitPredator(Predator p)
            {
                Console.WriteLine("Злой хищник");
            }
            public void VisitHerbore(Herbore h)
            {
                Console.WriteLine("Злой травоед");
            }
        }


        public class PanicPattern : IBehaviorVisitor
        {
            public void VisitPredator(Predator p)
            {
                Console.WriteLine("Напуганый хищник");
            }
            public void VisitHerbore(Herbore h)
            {
                Console.WriteLine("Напуганный травоед");
            }
        }


        public interface BehaviorElement
        {
            public void AcceptBehaviorPattern(IBehaviorVisitor visitor);
        }


        public abstract class IAnimal : IDamageable, BehaviorElement
        {
            public abstract void AcceptBehaviorPattern(IBehaviorVisitor visitor);
            public IAnimal(int health) : base(health) { }
        }


        public class Predator : IAnimal
        {
            public override void AcceptBehaviorPattern(IBehaviorVisitor v)
            {
                v.VisitPredator(this);
            }
            public Predator(int health = 40) : base(health) { }
        }


        public class Herbore : IAnimal
        {
            public override void AcceptBehaviorPattern(IBehaviorVisitor v)
            {
                v.VisitHerbore(this);
            }
            public Herbore(int health = 10) : base(health) { }
        }


        public abstract class Weapon
        {
            public virtual int Damage { get; protected set; }
        }


        public abstract class IDamageable
        {
            public int Health { get; private set; }

            public virtual void ApplayDamage(Weapon weapon)
            {
                if (Health == 0) return;
                Health -= Math.Abs(weapon.Damage);
                if (Health <= 0) Health = 0;
            }

            public IDamageable(int health)
            {
                Health = health;
            }
        }
        
        public class Knife : Weapon
        {
            public Knife(int damage = 0)
            {
                Damage = Math.Abs(damage);
            }
        }
    }
        
 }