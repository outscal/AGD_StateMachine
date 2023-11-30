namespace StatePattern.StateMachine
{
    public enum States
    {
        IDLE, // hitman, onepunchman, patrolman, robot, BOSS
        ROTATING, // onepunchman
        SHOOTING, //hitman, onepunchman, patrolman, robot
        PATROLLING, // hitman, patrolman, robot
        CHASING, // hitman, patrolman, robot, BOSS
        TELEPORTING, // hitman, robot, BOSS
        CLONING, // robot
        ROARING_INTIMIDATION, // BOSS
        QUADRUPLE_ATTACK, //BOSS
        FIRE_BREATH, // BOSS
        SUMMONING, // BOSS
        ULTIMATE // BOSS
    }
}