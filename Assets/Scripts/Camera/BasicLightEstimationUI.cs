using System;
using UnityEngine.UI;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// A simple UI controller to display basic light estimation information.
    /// </summary>
    [RequireComponent(typeof(BasicLightEstimation))]
    public class BasicLightEstimationUI : MonoBehaviour
    {
        [Tooltip("The UI Text element used to display the estimated ambient intensity in the physical environment.")]
        [SerializeField]
        Text m_AmbientIntensityText;

        /// <summary>
        /// The UI Text element used to display the estimated ambient intensity value.
        /// </summary>
        public Text ambientIntensityText
        {
            get { return m_AmbientIntensityText; }
            set { m_AmbientIntensityText = ambientIntensityText; }
        }

        void Awake()
        {
            m_BasicLightEstimation = GetComponent<BasicLightEstimation>();
        }

        void Update()
        {
            SetUIValue(m_BasicLightEstimation.brightness, ambientIntensityText);

        }
        
        void SetUIValue<T>(T? displayValue, Text text) where T : struct
        {
            if (text != null)
                text.text = displayValue.HasValue ? displayValue.Value.ToString(): "Unavailable";
        }

        BasicLightEstimation m_BasicLightEstimation;
    }
}
