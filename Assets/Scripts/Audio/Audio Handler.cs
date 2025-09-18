using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;


public class AudioHandler : MonoBehaviour{
    [SerializeField]
    private AudioSource sound;

    public void playSound(AudioClip audioClip, Transform position, float volume){
        AudioSource audioSource = Instantiate(sound, position.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject,audioSource.clip.length);
    }
    
    public void playRandomSound(AudioClip[] audioClip, Transform position, float volume){
        AudioSource audioSource = Instantiate(sound, position.position, Quaternion.identity);
        int rand = Random.Range(0, audioClip.Length);
        audioSource.clip = audioClip[rand];
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject,audioSource.clip.length);
    }
    public void setMusic(AudioClip audioClip, float volume){
        AudioSource [] worldSound = this.GetComponents<AudioSource>();
        worldSound[0].clip = audioClip;
        worldSound[0].volume = volume;
        worldSound[0].Play();
    }
    
    public void FaidBetweenWorldSound(AudioClip audioClip, float gollVolume, float speed, int index){
        StartCoroutine(FaidBetween(audioClip,gollVolume,speed,index));
    }

    IEnumerator FaidBetween(AudioClip audioClip, float gollVolume, float speed, int index) {
        AudioSource[] worldSound = this.GetComponents<AudioSource>();
        float volume = worldSound[index].volume;
        while (volume > 0) {
            volume -= speed / 100;
            worldSound[index].volume = volume;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        worldSound[index].clip = audioClip;
        worldSound[index].Play();
        while(volume < gollVolume){
            volume += speed/100;
            worldSound[index].volume = volume;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        
    }

    public void FaidInWorldSound(float gollVolume, float speed, int index)
    {
        AudioSource[] worldSound = this.GetComponents<AudioSource>();
        StartCoroutine(FaidIn(worldSound, gollVolume, speed, index));
    }
    public void FaidInWorldSound(AudioClip audioClip, float gollVolume, float speed, int index)
    {
        AudioSource[] worldSound = this.GetComponents<AudioSource>();
        worldSound[index].clip = audioClip;
        StartCoroutine(FaidIn(worldSound, gollVolume, speed, index));
    }

    IEnumerator FaidIn( AudioSource[] worldSound,float gollVolume, float speed,int index){
        
        float volume = 0;
        worldSound[index].volume = volume;
        worldSound[index].Play();
        while(volume < gollVolume){
            volume += speed/100;
            worldSound[index].volume = volume;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    
    public void FaideOutWorldSound(float speed, int index) {
        StartCoroutine(FaidOut(speed, index));
    }
    IEnumerator FaidOut(float speed,int index){
        AudioSource[] worldSound = this.GetComponents<AudioSource>();
        float volume = worldSound[index].volume;
        while(volume > 0){
            volume -=  speed/100;
            worldSound[index].volume = volume;
            yield return new WaitForSecondsRealtime(0.1f);
        } 
        worldSound[index].Pause();
    }
    public void setSoundEffect(AudioClip audioClip, float volume)
    {
        AudioSource[] worldSound = this.GetComponents<AudioSource>();
        worldSound[1].clip = audioClip;
        worldSound[1].volume = volume;
        worldSound[1].Play();
    }
    public void WorldSoundOn(bool state,int index){
        AudioSource [] music = this.GetComponents<AudioSource>();
        if(state){
            music[index].Play();
        }else{
            music[index].Pause();
        }
       
    }
    public AudioClip GetClip(int index){
        AudioSource [] music = this.GetComponents<AudioSource>();
        return music[index].clip;
    }
    public void SetLoop(bool state){
        AudioSource [] music = this.GetComponents<AudioSource>();
        music[1].loop = state;
    }
        
}
