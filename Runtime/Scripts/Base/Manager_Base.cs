using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    public class Manager_Base : MonoBehaviour
    {
        [Dropdown(Toolkit_Manager.KEY)]
        public List<string> dependencies = new();
    }
}