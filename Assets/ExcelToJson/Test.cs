//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Newtonsoft.Json;
//using s7u.dtb.exceldata;
//using UnityEngine;

//public class Test : MonoBehaviour
//{
//    public TextAsset mTextJson;
//    // Start is called before the first frame update
//    void Start()
//    {
//        DbManager dbManager = new DbManager();
//        List<Dictionary<string, string>> mItemDataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(mTextJson.text);
//        foreach(var v in mItemDataList)
//        {
//            Type mType = System.Type.GetType("s7u.dtb.exceldata.ConditionData");
//            DbBase mSheet = Activator.CreateInstance(mType) as DbBase;
//            mSheet.SetDbValue(v);
//            dbManager.addDb(mType, mSheet);
//        }

//        List<ConditionData> mItemList = dbManager.GetDb<ConditionData>();
//        foreach(var v in mItemList)
//        {
//            v.PrintDbInfo();
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
