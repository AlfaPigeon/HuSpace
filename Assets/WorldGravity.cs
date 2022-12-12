using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorldGravity : MonoBehaviour
{
    public GameObject[] virtual_cams;
    public static WorldGravity instance;
    public PlayerScript player;
    public Transform gravity_point;
    public float gravity_power = 15f;
    public SpaceMass Player_spacemass;
    private int active_index = 0;
    private float maxEnemyCounter = 20f;

    private float startTime = 0f;


    public GameObject[] enemies;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerScript>();
        Player_spacemass = player.GetComponent<SpaceMass>();
        Player_spacemass.Active_vector_forward = Vector3.forward;
        Player_spacemass.Active_vector_right = Vector3.right;
        Player_spacemass.Active_vector_up = Vector3.up;


        startTime = Time.time;
    }



    private IEnumerator SpawnEnemies()
    {
        while (true)
        {

            float timer = maxEnemyCounter - (Time.time - startTime) / 20;
            yield return new WaitForSeconds(timer > 0 ? timer:0.5f) ;

            Instantiate(enemies[Random.Range(0, enemies.Length)],new Vector3(Random.Range(0,25), Random.Range(0, 25), Random.Range(0, 25)) , Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        //Physics.gravity = (gravity_point.position - player.transform.position).normalized * gravity_power;

        foreach (SpaceMass sm in FindObjectsOfType<SpaceMass>()) {
            Rigidbody rb = sm.GetComponent<Rigidbody>();
            rb.AddForce( (gravity_point.position - rb.transform.position).normalized * gravity_power,ForceMode.Acceleration);
            
            float _up = Vector3.Distance(rb.transform.position, gravity_point.position + Vector3.up * 100f);
            float min = _up;
            float _down = Vector3.Distance(rb.transform.position, gravity_point.position + Vector3.down * 100f);
            if(_down < min)min = _down;
            float _left = Vector3.Distance(rb.transform.position, gravity_point.position + Vector3.left * 100f);
            if (_left < min) min = _left;
            float _right = Vector3.Distance(rb.transform.position, gravity_point.position + Vector3.right * 100f);
            if (_right < min) min = _right;
            float _forward = Vector3.Distance(rb.transform.position, gravity_point.position + Vector3.forward * 100f);
            if (_forward < min) min = _forward;
            float _back = Vector3.Distance(rb.transform.position, gravity_point.position + Vector3.back * 100f);
            if (_back < min)
            {
                SwitchToGravity(sm, 1);
                continue;
            }

            if(_up == min)
            {
                SwitchToGravity(sm, 0);
             
            }
            else if(_down == min)
            {
                SwitchToGravity(sm, 2);
              
            }
            else if(_left == min)
            {
                SwitchToGravity(sm, 4);
                
            }
            else if(_right == min)
            {
                SwitchToGravity(sm, 5);
              
            }
            else
            {
                SwitchToGravity(sm, 3);
                
            }
        }

    }




    public void SwitchToGravity(SpaceMass sm, int _gravity)
    {






            Debug.Log("Changing to gravity index " + _gravity);
            switch (_gravity)
        {
                
            case 0:

                    // Physics.gravity = new Vector3(0, -9.81f, 0);

                    sm.Active_vector_forward = Vector3.forward;
                    sm.Active_vector_right = Vector3.right;
                    sm.Active_vector_up = Vector3.up;
                    break;

                case 1:

                    //Physics.gravity = new Vector3(0, 0, 9.81f);

                    sm.Active_vector_forward = Vector3.up;
                    sm.Active_vector_right = Vector3.right;
                    sm.Active_vector_up = Vector3.back;




                    break;

                case 2:
                    //Physics.gravity = new Vector3(0, 9.81f, 0);

                    sm.Active_vector_forward = Vector3.back;
                    sm.Active_vector_right = Vector3.right;
                    sm.Active_vector_up = Vector3.down;




                    break;

                case 3:
                    //Physics.gravity = new Vector3(0, 0, -9.81f);

                    sm.Active_vector_forward = Vector3.down;
                    sm.Active_vector_right = Vector3.right;
                    sm.Active_vector_up = Vector3.forward;

                    break;

                case 4:
                    //left square
                    //Physics.gravity = new Vector3(9.81f, 0, 0);

                    sm.Active_vector_forward = Vector3.forward;
                    sm.Active_vector_right = Vector3.up;
                    sm.Active_vector_up = Vector3.left;


                    break;

                case 5:
                    //right square
                    //Physics.gravity = new Vector3(-9.81f,0, 0);


                    sm.Active_vector_forward = Vector3.forward;
                    sm.Active_vector_right = Vector3.down;
                    sm.Active_vector_up = Vector3.right;


                    break;

            }

   

        if(sm.transform.up != sm.Active_vector_up)
        {
            sm.transform.up = sm.Active_vector_up;
        }

            if (sm.gameObject.tag == "Player" && active_index != _gravity)
        {



            /*
            virtual_cams[_gravity].transform.rotation = virtual_cams[active_index].transform.rotation;

            switch (active_index)
            {

                case 0:

                    if (_gravity == 2)
                    {
                        virtual_cams[_gravity].transform.Rotate(90f, 0f, 0f);
                    }
                    else if(_gravity == 1)
                    {
                        virtual_cams[_gravity].transform.Rotate(-90f, 0f, 0f);

                    }
                    else if (_gravity == 4)
                    {

                        virtual_cams[_gravity].transform.Rotate(0f, 0f, 90f);
                    }
                    else if(_gravity == 5)
                    {
                        virtual_cams[_gravity].transform.Rotate(0f, 0f, -90f);
                    }

                    break;


                case 1:
                    if (_gravity == 3)
                    {
                        virtual_cams[_gravity].transform.Rotate(-90f, 0f, 0f);
                    }
                    else if (_gravity == 0)
                    {
                        virtual_cams[_gravity].transform.Rotate(90f, 0f, 0f);

                    }
                    else if (_gravity == 4)
                    {

                        virtual_cams[_gravity].transform.Rotate(0f, 0f, 90f);
                    }
                    else if (_gravity == 5)
                    {
                        virtual_cams[_gravity].transform.Rotate(0f, 0f, -90f);
                    }
                    break;


                case 2:
                    if (_gravity == 0)
                    {
                        virtual_cams[_gravity].transform.Rotate(-90f, 0f, 0f);
                    }
                    else if (_gravity == 3)
                    {
                        virtual_cams[_gravity].transform.Rotate(90f, 0f, 0f);

                    }
                    else if (_gravity == 4)
                    {

                        virtual_cams[_gravity].transform.Rotate(0f, 0f, -90f);
                    }
                    else if (_gravity == 5)
                    {
                        virtual_cams[_gravity].transform.Rotate(0f, 0f, 90f);
                    }
                    break;


                case 3:
                    if (_gravity == 2)
                    {
                        virtual_cams[_gravity].transform.Rotate(90f, 0f, 0f);
                    }
                    else if (_gravity == 0)
                    {
                        virtual_cams[_gravity].transform.Rotate(-90f, 0f, 0f);

                    }
                    else if (_gravity == 4)
                    {

                        virtual_cams[_gravity].transform.Rotate(0f, 0f, -90f);
                    }
                    else if (_gravity == 5)
                    {
                        virtual_cams[_gravity].transform.Rotate(0f, 0f, -+90f);
                    }
                    break;


                case 4:
                    if (_gravity == 0)
                    {
                        virtual_cams[_gravity].transform.Rotate(0f, 0f, 90f);
                    }
                    else if (_gravity == 2)
                    {
                        virtual_cams[_gravity].transform.Rotate(0, 0f, -90f);

                    }
                    else if (_gravity == 3)
                    {

                        virtual_cams[_gravity].transform.Rotate(90f, 0f, 0f);
                    }
                    else if (_gravity == 1)
                    {
                        virtual_cams[_gravity].transform.Rotate(-90f, 0f,0f );
                    }
                    break;


                case 5:
                    if (_gravity == 0)
                    {
                        virtual_cams[_gravity].transform.Rotate(0f, 0f, -90f);
                    }
                    else if (_gravity == 2)
                    {
                        virtual_cams[_gravity].transform.Rotate(0f, 0f, 90f);

                    }
                    else if (_gravity == 1)
                    {

                        virtual_cams[_gravity].transform.Rotate(-90f, 0f, 0f);
                    }
                    else if (_gravity == 5)
                    {
                        virtual_cams[_gravity].transform.Rotate(90f, 0f, 0f);
                    }
                    break;
            }


            */

            virtual_cams[_gravity].SetActive(true);
            virtual_cams[active_index].SetActive(false);
            active_index = _gravity;
            player.UpdateMovementReference();
        }


    }

}
