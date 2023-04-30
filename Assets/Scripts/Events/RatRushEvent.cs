using UnityEngine;

public class RatRushEvent : MonoBehaviour
{
    [SerializeField] private Transform Rats;
    [SerializeField] private Transform target;
    [SerializeField] private SoundService soundService;
    [SerializeField] private SoundType soundToPlay;

    private float speed = 7.5f;
    private bool rushActive = false;
    private bool reachedTarget = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null)
        {
            EventService.Instance.RatRushEvent.InvokeEvent();
            OnRatRush();
            soundService.PlaySoundEffects(soundToPlay);
            GetComponent<Collider>().enabled = false;
        }
    }

    void Update()
    {
        if (rushActive)
        {
            if (!reachedTarget)
            {
                Rats.position = Vector3.MoveTowards(Rats.position, target.position, speed * Time.deltaTime);

                if (Rats.position == target.position)
                {
                    reachedTarget = true;
                }
            }
            else
            {
                rushActive = false;
                Rats.gameObject.SetActive(false);
            }
        }
    }

    private void OnRatRush()
    {
        Debug.Log("OnRatRush Event Occurred.");
        Rats.gameObject.SetActive(true);
        rushActive = true;
    }
}
