using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData.Item
{
    public class ItemData
    {
        string Id;
        string ItemName;
        string ItemDescription;

        public ItemData(string id,string name,string description,string sprite)
        {
            Id = id;
            ItemName = name;
            ItemDescription = description;

            //Next step put only id in variable to be able to load name,description
            //not sure we will use csv or json
        }
    }
}
