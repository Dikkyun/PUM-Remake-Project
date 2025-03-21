using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWobble : MonoBehaviour
{
    public TMP_Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo= textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 10f, 0);
                
            }
        }
    }
}
