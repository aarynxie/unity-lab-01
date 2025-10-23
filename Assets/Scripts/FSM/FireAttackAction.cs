using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/FireAttack")]
public class FireAttackAction : Action
{
    public int maxPrefabInScene = 3;
    public float impulseForce = 1;
    public float degree = 45;
    public GameObject attackPrefab;
    // a SO updated by playermovement to store current mario's facing
    public BoolVariable marioFaceRight;

    public override void Act(StateController controller)
    {
        GameObject[] instantiatedPrefabsInScene = GameObject.FindGameObjectsWithTag(attackPrefab.tag);
        if (instantiatedPrefabsInScene.Length > maxPrefabInScene)
        {
            // instantiate it where controller (mario) is
            GameObject x = Instantiate(attackPrefab, controller.transform.position, Quaternion.identity);

            // get the rb component of instantiated attack prefab
            Rigidbody2D rb = x.GetComponent<Rigidbody2D>();
            // check if the rigidbody component exists
            if (rb != null)
            {
                // compute direction vector
                Vector2 direction = CalculateDirection(degree, marioFaceRight.Value);
                // apply a rightward impulse force to the object
                rb.AddForce(direction * impulseForce, ForceMode2D.Impulse);
            }
        }
    }

    public Vector2 CalculateDirection(float degrees, bool isFacingRight)
    {
        // convert degrees to radians
        float radians = degrees * Mathf.Deg2Rad;

        // calcualte the direction vector
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);

        // if the object is facing left, invert the x component of the direction
        if (!isFacingRight)
        {
            x = -x;
        }
        return new Vector2(x, y);
    }
}
