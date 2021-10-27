using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MAG.Popups;
using MAG.Utils;
using UnityEngine;

namespace MAG.Game
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }

        private readonly Dictionary<Type, IManager> managers = new Dictionary<Type, IManager>();
        private readonly List<IPopup> popups = new List<IPopup>();

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            RegisterManager(new PopupManager());

            GetComponentsInChildren(true, popups);
            for (int i = 0; i < popups.Count; i++)
            {
                var popup = popups[i];
                Get<PopupManager>().RegisterPopup(popup);
            }
        }

        private void Start()
        {
            Get<PopupManager>().Open<SplashPopup>().Forget();
        }

        private void OnDestroy()
        {
            Get<PopupManager>().Dispose();
        }

        public static T Get<T>() where T : IManager
        {
            return (T) Instance.Get(typeof(T));
        }

        private IManager Get(Type type)
        {
            return Instance.managers[type];
        }

        private void RegisterManager(IManager manager)
        {
            var managerType = manager.GetType();

            void AddToDictionary(Dictionary<Type, IManager> dict)
            {
                dict[managerType] = manager;
            }

            AddToDictionary(managers);
        }
    }
}
