using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image barImage;
    [SerializeField] private GameObject hasProgressGameObejct;
    private IHasProgress hasProgress;

    private void Start(){
        hasProgress = hasProgressGameObejct.GetComponent<IHasProgress>();
        if(hasProgress == null){
            Debug.LogError("GameObject" + hasProgressGameObejct+"doesnt have a component implementing IhasProgress interface");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0;
        Hide();
    }
    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e){
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0f || e.progressNormalized == 1f){
            Hide();
        }
        else{
            Show();
        }

    }
    private void Show(){
        gameObject.SetActive(true);

    }
    private void Hide(){
        gameObject.SetActive(false);

    }
    

}
