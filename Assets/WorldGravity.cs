using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGravity : MonoBehaviour
{
    public GameObject[] virtual_cams;
    public static WorldGravity instance;
    public PlayerScript player;
    public Transform gravity_point;
    public float gravity_power = 9.81f;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerScript>();
        Active_vector_forward = Vector3.forward;
        Active_vector_right = Vector3.right;
        Active_vector_up = Vector3.up;
    }

    public Vector3 Active_vector_forward;
    public Vector3 Active_vector_right;
    public Vector3 Active_vector_up;



    private void FixedUpdate()
    {
        Physics.gravity = (gravity_point.position - player.transform.position).normalized * gravity_power;
    }
    public void SwitchToGravity(GameObject target, int _gravity)
    {




        if (target.tag == "Player") { 
            PlayerScript _player = target.GetComponent<PlayerScript>();

            for (int i = 0; i < virtual_cams.Length; i++)
            {
                if (i == _gravity)
                    virtual_cams[i].SetActive(true);
            }


            for (int i = 0; i < virtual_cams.Length; i++)
            {
                if (i != _gravity)
                    virtual_cams[i].SetActive(false);
            }

            Debug.Log("Changing to gravity index " + _gravity);
            switch (_gravity)
        {
                
            case 0:
                   
                   // Physics.gravity = new Vector3(0, -9.81f, 0);

                    Active_vector_forward = Vector3.forward;
                    Active_vector_right = Vector3.right;
                    Active_vector_up = Vector3.up;
                    break;

                case 1:
                    
                    //Physics.gravity = new Vector3(0, 0, 9.81f);

                    Active_vector_forward = Vector3.up;
                    Active_vector_right = Vector3.right;
                    Active_vector_up = Vector3.back;




                    break;

                case 2:
                    //Physics.gravity = new Vector3(0, 9.81f, 0);

                    Active_vector_forward = Vector3.back;
                    Active_vector_right = Vector3.right;
                    Active_vector_up = Vector3.down;




                    break;

                case 3:
                    //Physics.gravity = new Vector3(0, 0, -9.81f);

                    Active_vector_forward = Vector3.down;
                    Active_vector_right = Vector3.right;
                    Active_vector_up = Vector3.forward;

                    break;

                case 4:
                    //left square
                    //Physics.gravity = new Vector3(9.81f, 0, 0);

                    Active_vector_forward = Vector3.forward;
                    Active_vector_right = Vector3.up;
                    Active_vector_up = Vector3.left;


                    break;

                case 5:
                    //right square
                    //Physics.gravity = new Vector3(-9.81f,0, 0);


                    Active_vector_forward = Vector3.forward;
                    Active_vector_right = Vector3.down;
                    Active_vector_up = Vector3.right;


                    break;

            }


            target.transform.forward = Active_vector_forward;
            target.transform.right = Active_vector_right;
            target.transform.up = Active_vector_up;
            _player.UpdateMovementReference();
        }


    }

}
