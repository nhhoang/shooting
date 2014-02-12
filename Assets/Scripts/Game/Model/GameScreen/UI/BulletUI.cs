using UnityEngine;
using System.Collections;

public class BulletUI : MonoBehaviour {
	public UILabel lblQuantity;
	public UIEventTrigger trigger;
	public UISprite sprite;
	
	public Bullet.Type bulletType = Bullet.Type.NORMAL;
	private float secondsToReload = 1.0f;
	private int quantity = 0;
	
	public void Init(Bullet.Type type, int quantity) {
		bulletType = type;
		this.quantity += quantity;
		lblQuantity.text = this.quantity.ToString();
		
		switch (bulletType) {
			case Bullet.Type.NORMAL:
				sprite.spriteName = "circles_11";
				secondsToReload = 1.5f;
			break;
			
			case Bullet.Type.RAPID:
				sprite.spriteName = "star_11";
				secondsToReload = 0.3f;
			break;
			
			case Bullet.Type.BIG_EXPLOSION:
				sprite.spriteName = "death_11";
				secondsToReload = 1.5f;
			break;
		}
	}
	
	public void Reload() {
		quantity--;
		lblQuantity.text = this.quantity.ToString();
		LeanTween.value(gameObject, FillReload, 0.0f, 1.0f, secondsToReload);
	}
	
	public void FillReload(float value) {
		sprite.fillAmount = value;
	}
	
	public void EventSelectBullet() {
		BulletManager.Instance.SetCurrentBulletType(bulletType, 0);
	}
}
