using System;
using System.Text;
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
        
        private readonly float updateInterval = 0.1f; 
        private float accum = 0.0f;
        private int frames = 0;
        private float timeleft;

        private float fps;

        private void Start()
        {
            timeleft = updateInterval;
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

            txtFPS.text = Mathf.Ceil(CalculateFPS()).ToString();
        }

        private float CalculateFPS()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            if (timeleft <= 0.0)
            {
                fps = (accum / frames);
                timeleft = updateInterval;
                accum = 0.0f;
                frames = 0;
            }

            return fps;
        }
    }
}