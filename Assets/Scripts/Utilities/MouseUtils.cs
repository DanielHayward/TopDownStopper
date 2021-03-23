using UnityEngine;
using UnityEngine.InputSystem;

namespace DKH.Utilis
{
    public static class MouseUtils
    {
        private static Camera camera;
        private static Mouse mouse;
        public static Vector3 GetWorldPosition(Vector2 position)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            Vector3 worldPosition = camera.ScreenToWorldPoint(position);
            worldPosition.z = 0f;
            return worldPosition;
        }
        public static Vector3 GetMouseWorldPosition2D()
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            if (mouse == null)
            {
                mouse = Mouse.current;
            }
            Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(new Vector3(mouse.position.ReadValue().x, mouse.position.ReadValue().y, -camera.transform.position.z));
            mouseWorldPosition.z = 0f;
            return mouseWorldPosition;
        }
        public static Vector3 GetMouseWorldPosition3D()
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            if (mouse == null)
            {
                mouse = Mouse.current;
            }
            //Physics.Raycast(camera.ScreenPointToRay(new Vector3(mouse.position.ReadValue().x, mouse.position.ReadValue().y, 0)), out RaycastHit hit, 2000);
            Ray vec = camera.ScreenPointToRay(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 10000000));
            Debug.DrawRay(vec.origin, vec.direction);
            Debug.DrawRay(vec.origin, Vector3.zero);
            RaycastHit hitInfo;
            Physics.Raycast(vec, out hitInfo);
            return new Vector3(hitInfo.point.x, hitInfo.point.y, 0);
            //return new Vector3(hit.point.x, hit.point.y, 0);
        }
    }
}