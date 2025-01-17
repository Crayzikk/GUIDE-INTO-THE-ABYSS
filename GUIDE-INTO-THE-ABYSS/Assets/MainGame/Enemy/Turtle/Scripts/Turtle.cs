public class Turtle : Enemy
{
    protected override int health { get; set; } = 50;
    protected override int damage { get; set; } = 15;
    protected override float attackRate { get; set; } = 1f;
}
