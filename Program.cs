namespace SimpleFiniteState;

class Program
{
    public static void Main(String[] args)
    {
        SimpleFiniteState fsm = new SimpleFiniteState();
        fsm.ProcessEvent(SimpleFiniteState.Events.OnGuard);
        fsm.ProcessEvent(SimpleFiniteState.Events.AlertGuard);
        fsm.ProcessEvent(SimpleFiniteState.Events.BraveGuard);
        fsm.ProcessEvent(SimpleFiniteState.Events.Scared);
        fsm.ProcessEvent(SimpleFiniteState.Events.DeadGuard);
    }
    class SimpleFiniteState
    {
        //Gyldige tilstande
        public enum States { Guarding, Chasing, Combat, Fleeing, Dead };
        //Input / begivenheder
        public enum Events { OnGuard, AlertGuard, BraveGuard, Scared, DeadGuard };
        public States State { get; private set; }


        private Action[,] fsm;

        public SimpleFiniteState()
        {
            //Tabel over Gyldige tilstandsskift
            fsm = new Action[5, 5] {
            //OnGuard,          AlertGuard,                BraveGuard,         Scared,          DeadGuard
            {GuadringEntity,     SeesPlayer,               null,               null,             null},              
            {null,               SeesPlayer,               CombatPlayer,       null,             DeadPlayer },    
            {null,               FleeingPlayer,            CombatPlayer,       SeesPlayer,       DeadPlayer },
            {null,               null,                     CombatPlayer,       FleeingPlayer,    DeadPlayer },
            {null,               SeesPlayer,               CombatPlayer,       FleeingPlayer,    DeadPlayer }
            };  
        }
        public void ProcessEvent(Events theEvent)
        {
            Action action = fsm[(int)State, (int)theEvent];
            if(action != null){
                action.Invoke();
            }
            else{
                Console.WriteLine("Invalid transition: " + State + " -> " + theEvent);
            }
        }

        private void GuadringEntity() { State = States.Guarding; }
        private void SeesPlayer() { State = States.Chasing; }
        private void FleeingPlayer() { State = States.Fleeing; }
        private void CombatPlayer() { State = States.Combat; }
        private void DeadPlayer() { State = States.Dead; }

        
    }
}