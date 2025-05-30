﻿using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements.Experimental;
using UnityEngine.XR;
using static Platformer.IState;

namespace Platformer
{
    public class StateMachine
    {
        StateNode current;
        Dictionary<Type, StateNode> nodes = new();
        HashSet<ITransition> anyTransitions = new();


        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.To);

                current.State?.Update();
            }
        }

        public void FixedUpdate()
        {
            current.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            current = nodes[state.GetType()];
            current.State?.OnEnter();
        }

        void ChangeState(IState state)
        {
            if (state == current.State) return;

            var previousState = current.State;
            var nextState = nodes[state.GetType()].State;


            previousState?.OnExit();
            nextState?.OnEnter();
            current = nodes[state.GetType()];
            
        }

        ITransition GetTransition()
        {
            foreach (var transition in anyTransitions)
            {
                if (transition.Condition.Evauluate()) return transition;
            }
            return null;


        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

         public void AddAnyTransition(IState to, IPredicate condition)
        {
            anyTransitions.Add(item: new Transition(to: GetOrAddNode(to).State, condition));
        }
            StateNode GetOrAddNode(IState state)
        {
            var node = nodes.GetValueOrDefault(key: state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                nodes.Add(state.GetType(), node);
            }
            return node;

        }
        class StateNode
        {
            public IState State { get; }

            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(item:new Transition(to, condition));
            }
        }
    }
}

