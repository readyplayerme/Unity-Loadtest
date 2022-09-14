using System;
using System.Threading;
using System.Threading.Tasks;
using Siccity.GLTFUtility;
using UnityEngine;

namespace ReadyPlayerMe
{
    public class GltfUtilityAvatarImporter : IAvatarImporter
    {
        private const string TAG = nameof(GltfUtilityAvatarImporter);

        public int Timeout { get; set; }
        public Action<float> ProgressChanged { get; set; }

        public async Task<AvatarContext> Execute(AvatarContext context, CancellationToken token)
        {
            if (context.Bytes == null)
            {
                throw new NullReferenceException();
            }

            context.Data = await ImportModel(context.Bytes, token);
            return context;
        }

        public async Task<GameObject> ImportModel(byte[] bytes, CancellationToken token = new CancellationToken())
        {
            SDKLogger.Log(TAG, "Importing avatar from byte array.");

            try
            {
                // ReSharper disable once RedundantAssignment
                GameObject avatar = null;
#if UNITY_EDITOR || UNITY_WEBGL
                avatar = Importer.LoadFromBytes(bytes, new ImportSettings());
                await Task.Yield();
                ProgressChanged?.Invoke(1);
                return avatar;
#else
                var isImportDone = false;
                Importer.ImportGLBAsync(bytes, new ImportSettings(), (model) =>
                {
                    avatar = model;
                    isImportDone = true;
                }, OnProgressChanged);

                while (!isImportDone && !token.IsCancellationRequested)
                {
                    await Task.Yield();
                }

                return token.IsCancellationRequested ? default : avatar;
#endif
            }
            catch (Exception exception)
            {
                throw Fail(exception.Message);
            }
        }

        public async Task<GameObject> ImportModel(string path, CancellationToken token = new CancellationToken())
        {
            SDKLogger.Log(TAG, $"Importing avatar from path {path}");

            try
            {
                // ReSharper disable once RedundantAssignment
                GameObject avatar = null;
#if UNITY_EDITOR || UNITY_WEBGL
                avatar = Importer.LoadFromFile(path, new ImportSettings());
                await Task.Yield();
                ProgressChanged?.Invoke(1);
                return avatar;
#else
                var isImportDone = false;
                Importer.ImportGLBAsync(path, new ImportSettings(), (model) =>
                {
                    avatar = model;
                    isImportDone = true;
                }, OnProgressChanged);

                while (!isImportDone && !token.IsCancellationRequested)
                {
                    await Task.Yield();
                }

                return token.IsCancellationRequested ? default : avatar;
#endif
            }
            catch (Exception exception)
            {
                throw Fail(exception.Message);
            }
        }

        private void OnProgressChanged(float progress, ImportType _) => ProgressChanged?.Invoke(progress);

        private Exception Fail(string error)
        {
            var message = $"Failed to import glb model from bytes. {error}";
            SDKLogger.Log(TAG, message);
            throw new CustomException(FailureType.ModelImportError, message);
        }
    }
}
