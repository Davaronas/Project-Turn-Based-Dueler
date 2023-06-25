using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float gravity = -9.81f;
    [Space]
    [SerializeField] private float baseMovementSpeed = 20f;
    [SerializeField] private float jumpStrenght = 40f;
    [SerializeField] private float loseJumpLevelSpeed = 0.1f;
    [Space]
    
    [SerializeField] private float rotateSensitivity = 50f;
    [SerializeField] private int maxY_Rotation = 80;
    [SerializeField] private int minY_Rotation = -80;
    [Space]
    [SerializeField] private float positionSlerpTime = 0.5f;
    [SerializeField] private float rotationSlerpTime = 0.5f;







    private CharacterController cc = null;
    private Camera playerCamera = null;


    private int stopMovement = 0;



    private Vector3 movementVector_;
    private float horizontalInput_;
    private float verticalInput_;
    private Vector3 horizontalVector_;
    private Vector3 verticalVector_;
    private float movementMultiplier_;

    private Vector3 jumpVector_;
    private float jumpLevel_ = 0;


    private float x_MouseInput_ = 0;
    private float y_MouseInput_ = 0;
    private Vector3 rotationVector_ = Vector3.zero;
    private float yLook_ = 0;
    private float xLook_ = 0;
    private float currentCameraRotation_X = 0;




    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        jumpLevel_ = gravity;

        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (IsMovementAllowed())
        {

          
                    cc.Move(DetermineMovementVector());
            
             
            

            Rotation();

            if(cc.isGrounded)
            {
                jumpLevel_ = gravity;
            }

            jumpLevel_ -= Time.deltaTime * loseJumpLevelSpeed;
            jumpLevel_ = Mathf.Clamp(jumpLevel_, gravity, Mathf.Infinity);
          //  print(jumpLevel_);
        }

  
    }

   
        

    private Vector3 DetermineMovementVector()
    {
        

        horizontalInput_ = Input.GetAxis("Horizontal");
        verticalInput_ = Input.GetAxis("Vertical");

        movementMultiplier_ = Time.deltaTime * baseMovementSpeed;

        horizontalVector_ = transform.right * (horizontalInput_ * movementMultiplier_);
        verticalVector_ = transform.forward * (verticalInput_ * movementMultiplier_);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cc.isGrounded)
            {

                jumpLevel_ = jumpStrenght;
            }
       
        }

        jumpVector_ = transform.up * jumpLevel_ * Time.deltaTime;

        movementVector_ = horizontalVector_ + verticalVector_ + jumpVector_;

        return movementVector_;
    }


    #region Rotation
    private void Rotation()
    {
        if(playerCamera == null) { Debug.LogError("There's no camera attached to the player"); return; }

        x_MouseInput_ = Input.GetAxis("Mouse X");
        y_MouseInput_ = Input.GetAxis("Mouse Y");

        RotatePlayer(x_MouseInput_);
        RotateCamera(y_MouseInput_);
    }

    public void RotatePlayer(float _x)
    {

        if (_x == 0) { return; }

        yLook_ = _x * rotateSensitivity * Time.deltaTime;

        rotationVector_ = transform.up * yLook_;

        transform.rotation *= Quaternion.Euler(rotationVector_);
    }

    public void RotateCamera(float _y)
    {

        xLook_ = _y * rotateSensitivity * Time.deltaTime;

        currentCameraRotation_X -= xLook_;
        currentCameraRotation_X = Mathf.Clamp(currentCameraRotation_X, minY_Rotation, maxY_Rotation);

     
        
            playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotation_X, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);
        
    


       // if (_y == 0) { return; }

       

      
      

        // playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotation_X, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);

        /*
        if (isWallRunning)
        {
            playerCamera.transform.localEulerAngles = Vector3.Slerp(new Vector3(playerCamera.transform.localEulerAngles.x, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z),
                new Vector3(currentCameraRotation_X, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z), wallRunCameraTurnSpeed * Time.deltaTime);
        }
        else
        {
            playerCamera.transform.localEulerAngles = Vector3.Slerp(new Vector3(playerCamera.transform.localEulerAngles.x, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z),
               new Vector3(currentCameraRotation_X, 0, 0), wallRunCameraTurnSpeed * Time.deltaTime);
        }
        */

        //playerCamera.transform.localEulerAngles = Vector3.Slerp(new Vector3(currentCameraRotation_X, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z),
        //   new Vector3(currentCameraRotation_X, 0f, 0f), wallRunCameraTurnSpeed * Time.deltaTime);

        // playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotation_X, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);
    }
    #endregion


   
    
  


    private bool IsMovementAllowed()
    {
        if (stopMovement == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }







    public void UpDraft(float _strenght, bool _releaseFromGround)
    {
        if(_releaseFromGround)
        {
            jumpLevel_ = jumpStrenght;
        }

        jumpLevel_ += _strenght;
    }

    

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //temp
        if (hit.transform.tag == "FallProtection")
        {
            transform.position = new Vector3(0, 1, 0);
        }

      
    }

   

    public void SetPositionAndRotation(Transform playerTargetTransform)
    {
        StartCoroutine(SlerpPosition(transform,transform.position, playerTargetTransform.position));
        StartCoroutine(SlerpRotation(transform,transform.rotation, playerTargetTransform.rotation));
        StartCoroutine(SlerpRotation(playerCamera.transform, playerCamera.transform.rotation, playerTargetTransform.rotation));

        //transform.position = playerTargetTransform.position;
        //transform.rotation = playerTargetTransform.rotation;
       // playerCamera.transform.rotation = playerTargetTransform.rotation;
    }

    public void SetRotation(Quaternion _rot)
    {
        StartCoroutine(SlerpRotation(transform, transform.rotation, _rot));
    }

    public void SetRotation(Quaternion _rot,float _speed)
    {
        StartCoroutine(SlerpRotation(transform, transform.rotation, _rot,_speed));
    }


    IEnumerator SlerpPosition(Transform _target,Vector3 _start,Vector3 _end)
    {
        float _progress = 0;

        while (_progress <= 1)
        {

            _target.position = Vector3.Slerp(_start, _end, _progress);
            _progress += positionSlerpTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _target.position = Vector3.Slerp(_start, _end, 1);
        yield return new WaitForEndOfFrame();
    }

    IEnumerator SlerpRotation(Transform _target,Quaternion _start,Quaternion _end)
    {
        float _progress = 0;

        

        while (_progress <= 1)
        {
            _target.rotation = Quaternion.Slerp(_start, _end, _progress);
            _progress += rotationSlerpTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _target.rotation = Quaternion.Slerp(_start, _end, 1);
        yield return new WaitForEndOfFrame();
    }

    IEnumerator SlerpRotation(Transform _target, Quaternion _start, Quaternion _end, float _speed)
    {
        float _progress = 0;



        while (_progress <= 1)
        {
            _target.rotation = Quaternion.Slerp(_start, _end, _progress);
            _progress += _speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _target.rotation = Quaternion.Slerp(_start, _end, 1);
        yield return new WaitForEndOfFrame();
    }







    public Vector3 GetPlayerVelocity()
    {
        return cc.velocity;
    }

   

   

  
}
