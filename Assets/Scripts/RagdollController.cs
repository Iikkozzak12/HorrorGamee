
using UnityEngine;


public class RagdollController : MonoBehaviour
{
    public Rigidbody[] rigidbodies;
    public Animator animator;
    public Collider[] colliders;
    public CharacterJoint[] joints;
    public GameObject[] CollidersToHide;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }

        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        joints = GetComponentsInChildren<CharacterJoint>();
        DeactivateRagdoll();
    }

#if UNITY_EDITOR
    public void RefreshInEditor()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        joints = GetComponentsInChildren<CharacterJoint>();
    }

#endif
    public void ToggleColliders(bool flag)
    {
        if (flag)
        {
            foreach (var item in CollidersToHide)
            {
                item.gameObject.SetActive(true);
            }

        }
        else if (!flag)
        {
            foreach (var item in CollidersToHide)
            {
                item.gameObject.SetActive(false);
            }
        }

    }
    [ContextMenu("Deactivate")]
    public void DeactivateRagdoll()
    {
        // Debug.Log("deactivating rg");
        foreach (var joint in joints)
        {



        }
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;


        }

        foreach (var c in colliders)
        {
            if (c is CharacterController) continue;
            c.isTrigger = true;


        }


        if (animator)
        {
            animator.enabled = true;
        }






    }

    [ContextMenu("Activate")]
    public void ActivateRagdoll()
    {

        if (animator)
        {
            animator.enabled = false;
        }


        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;

        }

        foreach (var c in colliders)
        {
            if (c is CharacterController)
            {
                c.enabled = false;
                continue;
            }

            c.isTrigger = false;

        }

    }



}
