using UnityEngine;
using UnityEngine.EventSystems;

public class OnMonsterClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject monster;
    public AudioClip tapOnMonsterClip;

    private float returnSizeTime = 0.5f;
    private float reducedSizeTime = 10f;
    private bool isStartAnimation = false;
    private Animator monsterAnimator;
    private AudioSource tapOnMonsterAS;

    public int ClickSum { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        tapOnMonsterAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= returnSizeTime && isStartAnimation)
        {
            monsterAnimator.ResetTrigger("Click");
            isStartAnimation = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickSum++;
        tapOnMonsterAS.PlayOneShot(tapOnMonsterClip, 1.5f);

        returnSizeTime = Time.time + reducedSizeTime;
        if (!(monsterAnimator == null))
        {
            monsterAnimator.SetTrigger("Click");
            isStartAnimation = true;
        }
    }
}
