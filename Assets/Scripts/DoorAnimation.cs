using UnityEngine;
using UnityEngine.UI;

public class DoorAnimation : MonoBehaviour
{
   [SerializeField] private Animator doorAnimationController;
   [SerializeField] private Toggle doorToggle;

   private bool _openDoor;
   private static readonly int Door = Animator.StringToHash("OpenDoor");

   private void Awake()
   {
      doorToggle.onValueChanged.AddListener(SetAnimationStatus);
   }

   private void SetAnimationStatus(bool status)
   {
      _openDoor = status;
   }

   private void Update()
   {
      if (_openDoor)
      {
         OpenDoor();
      }
      else
      {
         CloseDoor();
      }
   }

   private void CloseDoor()
   {
      doorAnimationController.SetBool(Door, false);
   }

   private void OpenDoor()
   {
      doorAnimationController.SetBool(Door, true);
   }
}
