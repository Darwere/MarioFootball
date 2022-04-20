using BehaviorTree;
using UnityEngine;

namespace GoalTreeSpace
{
    public class CanSaveGoal : Node
    {
        private Player goal;
        private float saveRange;
        private float mightSaveRange;

        private float intersectTime = 0;
        private Vector3 intersectPoint = Vector3.zero;

        public CanSaveGoal(Player goal, float saveRange, float mightSaveRange)
        {
            this.goal = goal;
            this.saveRange = saveRange;
            this.mightSaveRange = mightSaveRange;
        }

        public override NodeState Evaluate()
        {
            object value = GetData("canSave");

            if (value != null)
            {
                bool canSave = (bool)value;

                if (canSave)
                {
                    Debug.Log("Succes");
                    state = NodeState.Succes;
                    return state;
                }
                else
                {
                    Debug.Log("Failure");
                    state = NodeState.Failure;
                    return state;
                }
            }

            IntersectionTime();

            Vector3 direction = intersectPoint - goal.transform.position;
            float distance = Vector3.Distance(intersectPoint, goal.transform.position);
            float time = distance / goal.Species.speed;
            float ratio = Mathf.Min(intersectTime / time, 1);

            Vector3 position = goal.transform.position + direction * ratio;
            distance = Vector3.Distance(intersectPoint, position);
            Node root = GetRootNode();
            float interval = mightSaveRange - saveRange;
            float gap = Mathf.Max(0, distance - saveRange);
            float arrestPenality = gap / interval;
            float arrestRoll = Random.Range(0f, 1f);

            if (arrestRoll < 1f - arrestPenality)
            {
                Debug.Log("Succes");
                root.SetData("canSave", true);
                state = NodeState.Succes;
                return state;
            }
            else
            {
                Debug.Log("Failure");
                root.SetData("canSave", false);
                state = NodeState.Failure;
                return state;
            }
        }

        private void IntersectionTime()
        {
            Rigidbody ballRigidbody = Field.Ball.GetComponent<Rigidbody>();
            Vector3 acceleration = Physics.gravity;
            acceleration.x = Mathf.Abs(ballRigidbody.velocity.x);
            Vector3 position = Field.Ball.transform.position;
            position.x = Mathf.Abs(position.x);
            Vector3 velocity = ballRigidbody.velocity;
            velocity.x = Mathf.Abs(velocity.x);

            float goalPositionX = Mathf.Abs(goal.transform.position.x);

            float time = 0;

            while (position.x < goal.transform.position.x)
            {
                time += Time.deltaTime;
                position += velocity * Time.deltaTime;
                if(position.y > 0)
                    position += acceleration * Time.deltaTime;
            }

            if (ballRigidbody.velocity != velocity)
                position.x *= -1;

            intersectPoint = position;
            intersectTime = time;
        }
    }
}
