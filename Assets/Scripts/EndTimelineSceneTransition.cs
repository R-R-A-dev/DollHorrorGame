using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class EndTimelineSceneTransition : MonoBehaviour
{
    [SerializeField] private GameObject ownDoll;
    [SerializeField] private GameObject movieOwnDoll;
    [SerializeField] PlayableDirector timeline; // Timeline��PlayableDirector
    private string titleSceneName = "TitleScene";      // �J�ڐ�̃V�[����
    

    void Start()
    {

    }

    // Timeline���I��������V�[�����ړ�
    public void OnTimelineEnd(PlayableDirector director)
    {
        SceneManager.LoadScene(titleSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (timeline == null)
            {
                timeline = GetComponent<PlayableDirector>(); // PlayableDirector���擾
            }

            if (timeline != null)
            {
                ownDoll.SetActive(false); 
                movieOwnDoll.SetActive(true);
                FirstPersonController.freezeTrg = true; // �v���C���[�̓������~�߂�
                timeline.Play(); // Timeline���Đ�
            }
        }
    }
}