using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JariUnityUISystem
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<MenuNode> menus;
        [SerializeField] private List<MenuNode> _selectedMenus;
        private MenuNode _lowestPriority;
        
        private void Start()
        {
            AddLowestPriority();
            DeactivateAll();
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            foreach (var m in menus)
            {
                if (Input.GetKeyDown(m.button))
                {
                    HandleMenu(m);
                }
            }
        }

        private void HandleMenu(MenuNode menuNode)
        {
            //hier check je of de node dat is aangedrukt al actief is of niet.
            if (menuNode.activated == true)
            {
                //checked of er een menuNode in selectedNodes niet al op stopActivity staat.
                //checked of de menuNode waar op gedrukt is niet zelf op stopActivity staat.
                if (StopActivity() == false || menuNode.stopActivity)
                {
                    //als dit zo is dan zet hij hem op inactive
                    Deactivate(menuNode);
                }
            }
            else if(menuNode.activated == false)
            {
               //als de aangedrukte node niet actief is dan zet je hem op active met deze functie.
                ActivateMenu(menuNode);
            }
        }

        private bool StopActivity()
        {
            foreach (var s in _selectedMenus)
            {
                //als er een van de selectedMenus een boolean heeft waar de stopActivity true op staat
                //zet hij de functie StopActivity op true
                if (s.stopActivity)
                {
                    return true;
                }
            }
            return false;
        }


        private bool CheckPriority(MenuNode menuNode)
        {
            for (int i = 0; i < _selectedMenus.Count; i++)
            {
                if (menuNode.priority < _selectedMenus[i].priority)
                {
                    return true;
                }
            }
            return false;
        }

        #region menu activation methods
        private void ActivateMenu(MenuNode menuNode)
        {
            if (CheckPriority(menuNode) == false)
            {
                menuNode.Activate();
                _selectedMenus.Add(menuNode);
                   
            } 
        }

        private void Deactivate(MenuNode menuNode)
        {
            menuNode.Deactivate();
            _selectedMenus.Remove(menuNode);
        }

        public void DeactivateAll()
        {
            //pas op in welke volgorde je de UnityEvents gebruikt. 
            foreach (var m in menus)
            {
                Deactivate(m);
            }
        }

        #endregion

        public void ChangeTimeScale(float timeScaleSpeed)
        {
            Time.timeScale = timeScaleSpeed;
        }
        
        private void AddLowestPriority()
        {
            MenuNode lowestPriority = new MenuNode();
            lowestPriority.priority = int.MinValue;
            _selectedMenus.Add(lowestPriority);
        }

        public void ChangeObjectActivityState(GameObject panel)
        {
            panel.SetActive(!panel.activeSelf);
        }
        
        public void ActivateGameObject(GameObject panel) => panel.SetActive(true);
        public void DeactivateGameObject(GameObject panel) => panel.SetActive(false);

        public void DeactivateScript(Behaviour behaviour) => behaviour.enabled = false;
        public void ActivateScript(Behaviour behaviour) => behaviour.enabled = true;
    }
}
