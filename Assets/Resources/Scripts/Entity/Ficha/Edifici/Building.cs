using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Ficha
{
    public enum BuildingType { TRENCH,SNIPER,ATANK,AAIR, COUNT}
    protected BuildingType bType;
    protected List<Troop.troopType> _troopTypes;

    protected int _damage;

    protected static Sprite trench0, trench1, trench2;
    protected static Sprite sniper0, sniper1;
    protected static Sprite atank0, atank1, atank2;
    protected static Sprite aair;
    protected static Sprite upCammo; protected GameObject upCammoObj;
    protected static Sprite upArmor; protected GameObject upArmorObj;
    protected static Sprite upX2; protected GameObject upX2Obj;


    protected int _sprite_index;

    protected List<Ficha> _targets;

    protected Building()
    {
        if(aair == null)
        {
            trench0 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/trench0"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            trench1 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/trench1"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            trench2 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/trench2"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);

            sniper0 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/sniper0"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            sniper1 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/sniper1"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);

            atank0 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/atank0"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            atank1 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/atank1"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            atank2 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/atank2"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);

            aair = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/aair"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);

            upCammo = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/upgrade_cammo"), new Rect(0, 0, Constants.Entity.Building.up_wPix, Constants.Entity.Building.up_hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            upArmor = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/upgrade_armor"), new Rect(0, 0, Constants.Entity.Building.up_wPix, Constants.Entity.Building.up_hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            upX2 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/upgrade_x2"), new Rect(0, 0, Constants.Entity.Building.up_wPix, Constants.Entity.Building.up_hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Constants.Layers.zBuilding);

        upCammoObj = new GameObject("CammoUp"); 
        upArmorObj = new GameObject("ArmorUp");
        upX2Obj = new GameObject("X2Up");

        upCammoObj.AddComponent<SpriteRenderer>();
        upCammoObj.GetComponent<SpriteRenderer>().sprite = upCammo;
        upCammoObj.transform.SetParent(gameObject.transform);
        upCammoObj.transform.position = new Vector3(upCammoObj.transform.position.x - Constants.Entity.Building.up_scale / 1.5f,
                                                    upCammoObj.transform.position.y, 
                                                    Constants.Layers.zBuildingIndicator);
        upCammoObj.transform.localScale = new Vector3(Constants.Entity.Building.up_scale, Constants.Entity.Building.up_scale, 1);

        upArmorObj.AddComponent<SpriteRenderer>();
        upArmorObj.GetComponent<SpriteRenderer>().sprite = upArmor;
        upArmorObj.transform.SetParent(gameObject.transform);
        upArmorObj.transform.position = new Vector3(upArmorObj.transform.position.x + Constants.Entity.Building.up_scale / 1.5f,
                                                    upArmorObj.transform.position.y,
                                                    Constants.Layers.zBuildingIndicator);
        upArmorObj.transform.localScale = new Vector3(Constants.Entity.Building.up_scale, Constants.Entity.Building.up_scale, 1);

        upX2Obj.AddComponent<SpriteRenderer>();
        upX2Obj.GetComponent<SpriteRenderer>().sprite = upX2;
        upX2Obj.transform.SetParent(gameObject.transform);
        upX2Obj.transform.position = new Vector3(upX2Obj.transform.position.x,
                                                 upX2Obj.transform.position.y - Constants.Entity.Building.up_scale / 1.5f,
                                                 Constants.Layers.zBuildingIndicator);
        upX2Obj.transform.localScale = new Vector3(Constants.Entity.Building.up_scale, Constants.Entity.Building.up_scale, 1);

        upCammoObj.SetActive(false);
        upArmorObj.SetActive(false);
        upX2Obj.SetActive(false);

        type = Ficha_Type.EDIFICIO;
        targets = new List<Ficha>();
        troopTypes = new List<Troop.troopType>();
    }
    public virtual void nextSprite() { }
    public virtual void previousSprite() { }


    public List<Ficha> targets
    {
        get { return _targets; }
        set { _targets = value; }
    }

    public override List<Ficha> getTargets()
    {
        return targets;
    }
    public int sprite_index
    {
        get { return _sprite_index; }
        set { _sprite_index = value; }
    }
    public int damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    public List<Troop.troopType> troopTypes
    {
        get { return _troopTypes; }
        set { _troopTypes = value; }
    }
}
