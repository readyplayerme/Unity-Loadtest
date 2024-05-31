using System.Globalization;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

namespace ReadyPlayerMe.Loadtest.UI
{
    public class RenderStatsUI : MonoBehaviour
    {   
        [SerializeField] private Text txtSetPassCalls;
        [SerializeField] private Text txtDrawCall;
        [SerializeField] private Text txtVertices;
        [SerializeField] private Text txtFPS;

        private ProfilerRecorder setPassCallsRecorder;
        private ProfilerRecorder drawCallsRecorder;
        private ProfilerRecorder verticesRecorder;
        
        private const float UPDATE_INTERVAL = 0.1f; 
        private const float FPS_TEXT_UPDATE_RATE = 0.02f;
        private float accum = 0.0f;
        private int frames = 0;
        private float timeleft;
        private float lastFpsTextUpdate = 0.0f;

        private float fps;

        private void Start()
        {
            timeleft = UPDATE_INTERVAL;
            setPassCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
            drawCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
            verticesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
        }

        void OnDisable()
        {
            setPassCallsRecorder.Dispose();
            drawCallsRecorder.Dispose();
            verticesRecorder.Dispose();
        }

        void Update()
        {
            if (setPassCallsRecorder.Valid)
                txtSetPassCalls.text = setPassCallsRecorder.LastValue.ToString();
            if (drawCallsRecorder.Valid)
                txtDrawCall.text = drawCallsRecorder.LastValue.ToString();
            if (verticesRecorder.Valid)
                txtVertices.text = verticesRecorder.LastValue.ToString();

            if (!(Time.time - lastFpsTextUpdate > FPS_TEXT_UPDATE_RATE)) return;
            txtFPS.text = Mathf.Ceil(CalculateFPS()).ToString(CultureInfo.InvariantCulture);
            lastFpsTextUpdate = Time.time;
        }

        private float CalculateFPS()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            if (timeleft <= 0.0)
            {
                fps = (accum / frames);
                timeleft = UPDATE_INTERVAL;
                accum = 0.0f;
                frames = 0;
            }

            return fps;
        }
    }
}