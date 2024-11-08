using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("sceneChange", (int sceneIndex) =>
        {
            SceneManager.LoadScene(sceneIndex);
        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("sceneChange");
    }
}
