using UnityEngine;
using System.Collections;

namespace Game.Core{
    [ExecuteInEditMode]
    public class RenderDepth : MonoBehaviour
    {
        void OnEnable()
        {
            GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;
        }
    }
}
