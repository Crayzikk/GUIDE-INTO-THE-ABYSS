public class Minotaur : Enemy
{
    protected override int health { get; set; } = 200;
    protected override int damage { get; set; } = 20;
    protected override float attackRate { get; set; } = 2.5f;
}
