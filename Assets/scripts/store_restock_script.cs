using UnityEngine;

public class store_restock_script : MonoBehaviour
{
    Vector3 initpos;
    Quaternion initrot;
    [SerializeField] GameObject prefab, parent_gb;
    bool restocked = false;

    void Start()
    {
        initpos = transform.position;
        initrot = transform.rotation;
    }

    void Update()
    {
        if (restocked==false && Vector3.Distance(transform.position, initpos) > 1f)
        {
            var item_price=GetComponent<set_price_script>();
            var data=PlayerData_Save_manager.Instance.data;
            data.homecoins-=item_price.price;
            restocked = true;
            GameObject new_gb = Instantiate(prefab, initpos, initrot);
            var script = new_gb.AddComponent<store_restock_script>();
            script.prefab = prefab;
            script.parent_gb = parent_gb;
            if (parent_gb != null)
                new_gb.transform.SetParent(parent_gb.transform, true);
            var rb = new_gb.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;
            var new_item_price=new_gb.AddComponent<set_price_script>();
            new_item_price.price=item_price.price;
            gameObject.AddComponent<Item_SaveMyData>();
            Destroy(item_price);
            Destroy(this);
        }
    }
}
