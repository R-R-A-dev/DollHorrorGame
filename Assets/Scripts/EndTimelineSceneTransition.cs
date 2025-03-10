using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class EndTimelineSceneTransition : MonoBehaviour
{
    [SerializeField] private GameObject ownDoll;
    [SerializeField] private GameObject movieOwnDoll;
    [SerializeField] PlayableDirector timeline; // TimelineのPlayableDirector
    private string titleSceneName = "TitleScene";      // 遷移先のシーン名
    

    void Start()
    {

    }

    // Timelineが終了したらシーンを移動
    public void OnTimelineEnd(PlayableDirector director)
    {
        SceneManager.LoadScene(titleSceneName);
    }

    //FirstPersonControllerのスクリプトをFindして破棄
    //シーン遷移
    public void SceneTransition()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(titleSceneName);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (timeline == null)
            {
                timeline = GetComponent<PlayableDirector>(); // PlayableDirectorを取得
            }

            if (timeline != null)
            {
                FirstPersonController.freezeTrg = true; // プレイヤーの動きを止める
                timeline.Play(); // Timelineを再生
            }
        }
    }
}