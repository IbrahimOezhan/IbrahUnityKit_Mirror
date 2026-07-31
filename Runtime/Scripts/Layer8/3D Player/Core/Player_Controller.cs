#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Debugging;
using IbrahKit.Manager;
using IbrahKit.StateMachine;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.ThreeDPlayer
{
    public class Player_Controller : Manager_Local<Player_Controller>
    {
        [SerializeField, Required] private CharacterController cc;

        [SerializeField, Required] private Player_State firstState;

        [SerializeField, InlineProperty, FoldoutGroup("Handlers")]
        private Player_Gravity_Handler gravityHandler = new();

        [SerializeField,
         InlineProperty,
         FoldoutGroup("Handlers"),
         SerializeReference,
         TypeFilter(nameof(GetFilteredTypeList)),
         HideLabel]
        private List<Player_Input_Handler> inputHandler = new();

        [SerializeField, ReadOnly] private Player_State currentState;

        private Player3D_Input input;
        private MonoStateMachine<Player_State> machine;

        private void Start()
        {
            machine = new(firstState);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            gravityHandler.Update(this, deltaTime);

            if (machine != null)
            {
                machine.RunMachine();
                currentState = machine.GetState();
            }
            else
            {
                IbrahDebug.LogError("PlayerControllers state machine is null");
            }
        }

        private IEnumerable<Type> GetFilteredTypeList()
        {
            if (inputHandler == null)
            {
                IbrahDebug.LogError("Input Handler List is null");
                return new Type[] { };
            }

            int removed = inputHandler.RemoveAll(x => x == null);

            if (removed > 0) IbrahDebug.Log($"Removed {removed} null elements from Input Handler List");

            IEnumerable<Type> alreadyAddedInputHandlers = inputHandler.Select(x => x.GetType());

            IEnumerable<Type> inputHandlers = Type_Utilities.GetSubTypes(typeof(Player_Input_Handler))
                .Except(alreadyAddedInputHandlers).Except(new[] { typeof(Player_Gravity_Handler) });

            return inputHandlers;
        }

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            input = new();

            input.Enable();

            inputHandler.Add(gravityHandler);

            inputHandler.ForEach(x => x.Init(input));
        }

        public T GetHandler<T>() where T : Player_Input_Handler, new()
        {
            foreach (var t in inputHandler)
            {
                if (t.GetType() == typeof(T))
                {
                    return t as T;
                }
            }

            return null;
        }

        public CharacterController GetController() => cc;

        public Player_Gravity_Handler GetGravity() => gravityHandler;

        public Player_State GetState() => currentState;
    }
}