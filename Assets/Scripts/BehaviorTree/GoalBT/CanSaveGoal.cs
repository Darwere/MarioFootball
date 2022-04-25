using BehaviorTree;
using UnityEngine;
using System.Collections;

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
                    state = NodeState.Succes;
                    return state;
                }
                else
                {
                    state = NodeState.Failure;
                    return state;
                }
            }

            IntersectionTime();

            Vector3 direction = intersectPoint - goal.transform.position;
            direction.y = 0;
            direction = direction.normalized;
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

            Debug.Log("intercept Position" + intersectPoint + " intercept Time"+ intersectTime + " intercept Distance " + distance);
            if (arrestRoll < 1f - arrestPenality)
            {
                Debug.Log(goal.name + " Succes");
                root.SetData("canSave", true);
                root.SetData("destination", position);
                state = NodeState.Succes;
            }
            else
            {
                Debug.Log(goal.name + " Failure");
                root.SetData("canSave", false);
                state = NodeState.Failure;
            }
            return state;
        }

        private void IntersectionTime()
        {
            /*Rigidbody ballRigidbody = Field.Ball.GetComponent<Rigidbody>();
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
                     velocity += acceleration * Time.deltaTime;
                else
                {
                    velocity.x += acceleration.x * Time.deltaTime;
                    velocity.z += acceleration.z * Time.deltaTime;
                }
            }

            if (ballRigidbody.velocity != velocity)
                position.x *= -1;

            intersectPoint = position;
            intersectTime = time;*/

            Vector3 startPosition = Field.Ball.StartingPoint;
            Vector3 destination = Field.Ball.Destination;
            Vector3 bezierPoint = Field.Ball.BezierPoint;
            Vector3 ballPosition = startPosition;
            float duration = Field.Ball.Duration;
            float bezierPercent = 0;
            bool condition;
            Debug.Log(bezierPoint);
            do
            {
                bezierPercent += Time.deltaTime / duration;

                ballPosition = Mathf.Pow(1 - bezierPercent, 2f) * startPosition +
                    2 * (1 - bezierPercent) * bezierPercent * bezierPoint +
                    Mathf.Pow(bezierPercent, 2f) * destination;

                condition = goal.transform.position.x < 0 ?
                    goal.transform.position.x < ballPosition.x : goal.transform.position.x > ballPosition.x;

            } while(condition && bezierPercent < 1);

            intersectPoint = ballPosition;
            intersectTime = bezierPercent * duration;
        }

        private IEnumerator ClearCanSave()
        {
            yield return new WaitUntil(() => Field.Ball.GotShooted);
            Node root = GetRootNode();
            root.ClearData("canSave");
        }
    }
}
