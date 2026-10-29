
using UnityEngine;

public class Door : MonoBehaviour
{
    private UIController _uiCnt;　//UIControllerスクリプトを参照できる変数
    private string _id; //セーブ用ID

    void Start()
    {
        _uiCnt = GameObject.FindFirstObjectByType<UIController>();
        _id = SaveDataManager.MakeId(gameObject);
        if (SaveDataManager.IsUsed(_id))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 接触した相手がPlayerか確認
        if (collision.gameObject.tag == "Player")
        {
            // 鍵を1個以上持っている場合
            if (ItemKeeper.hasKeys >= 1.0f)
            {
                SoundManager.soundManager.SEPlay(SEType.DoorOpen);

                // 鍵を1個消費する
                ItemKeeper.hasKeys -= 1.0f;

                // UIの鍵表示を更新する
                _uiCnt.UpdateItemCount(ItemType.key, ItemKeeper.hasKeys);
                SaveDataManager.SetUsed(_id); //解錠したのでリストに刻まれる

                // ドアを消す
                Destroy(gameObject);
            }
            else
            {
                SoundManager.soundManager.SEPlay(SEType.DoorClosed);
            }
        }
    }
}
