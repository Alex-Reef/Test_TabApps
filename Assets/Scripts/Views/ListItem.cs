using Coffee.UIExtensions;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class ListItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private Image background;
        [SerializeField] private UIParticle particle;

        public void Init(DataModel dataModel)
        {
            title.text = dataModel.text;
            
            var color = dataModel.color;
            color.a = 1;
            background.color = color;
        }

        public void PlayAnimation()
        {
            particle.Play();
        }
    }
}