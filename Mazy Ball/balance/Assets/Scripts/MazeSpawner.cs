using UnityEngine;
public class MazeSpawner : MonoBehaviour {
	public enum MazeGenerationAlgorithm{
		RecursiveTree
	}
    MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.RecursiveTree;
    public GameObject Floor;
    public GameObject FloorWithHole;
    public GameObject Wall;
    public GameObject Target;
    public GameObject PowerUpObject;
    int Rows ;
	int Columns ;
	float CellWidth;
	float CellHeight;
    float yOffset = 1f;
    int powerUpSerial = 0;
	private BasicMazeGenerator mMazeGenerator = null;
	void Start () {
        CellHeight = CellWidth = Util.rowColumnHeightWidth;
        Rows = Mathf.Max( PlayerPrefs.GetInt("rowCount"),3);
        Columns = Mathf.Max(PlayerPrefs.GetInt("colCount"), 3);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Random.InitState(Random.Range(0, 10000));
        switch (Algorithm) {
		case MazeGenerationAlgorithm.RecursiveTree:
			mMazeGenerator = new RecursiveTreeMazeGenerator (Rows, Columns);
			break;
		}
		mMazeGenerator.GenerateMaze ();
		for (int row = 0; row < Rows; row++) {
			for(int column = 0; column < Columns; column++){
				float x = column*CellWidth;
				float z = row*CellHeight;
				MazeCell cell = mMazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
                if(row==Rows-1 && column == Columns - 1)
                {
                    Util.initialCameraPosition = new Vector3(x, -yOffset - 0.16f, z) + new Vector3(0, 2, 0);
                    Camera.main.transform.position = Util.initialCameraPosition;
                    tmp = Instantiate(Floor, new Vector3(x, -yOffset - 0.16f, z), Quaternion.Euler(-180, 0, 0))
                        as GameObject;
                    GameObject tmp1 =
                        Instantiate(Target, new Vector3(x - 1f, yOffset - 0.5f, z + 0.5f), Quaternion.Euler(-75, -135, 0))
                        as GameObject;
                    tmp1.transform.parent = transform;
                    Util.lastFloorPosition = new Vector3(x - 1f, 0, z + 0.5f);
                }
                else if (Random.Range(10, 20) > 12 || (row==0 & column==0))
                {
                    if (Random.Range(0, 100) > 75 && row!=0 && column!=0)
                    {
                        powerUpSerial++;
                        powerUpSerial %= 7;
                        GameObject tmpPowerUp = Instantiate(PowerUpObject, new Vector3(x, yOffset - 0.5f, z),
                            new Quaternion(0, 0, 0, 0));
                        tmpPowerUp.GetComponent<PowerUp>().powerUpCode = powerUpSerial;
                        tmpPowerUp.transform.parent = transform;
                        tmpPowerUp.GetComponent<MeshRenderer>().material.color = Util.powerUpColor[powerUpSerial];
                    }
                    tmp = Instantiate(Floor, new Vector3(x, yOffset, z), Quaternion.Euler(0, 0, 0))
                        as GameObject;
                    tmp.transform.name = row + "," + column;
                }
                else
                {
                    tmp = Instantiate(FloorWithHole, new Vector3(x, -yOffset-0.16f, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                    tmp.transform.localEulerAngles = new Vector3(0, 0, 180);
                    tmp.transform.name = row + "," + column;
                    
                }
                float colorRange = (float)(row + column) / (float)(Rows + Columns);
                tmp.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(colorRange,0.73f,0.96f); 
                tmp.transform.parent = transform;
				if(cell.WallRight){
					tmp = Instantiate(Wall,
                        new Vector3(x+CellWidth/2,-0.1f,z)+Wall.transform.position+Util.wallOffset,Quaternion.Euler(0,90,0))
                        as GameObject;// right
					tmp.transform.parent = transform;
                    tmp.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(colorRange, 0.73f, 0.96f);
                }
				if(cell.WallFront){
					tmp = Instantiate(Wall,
                        new Vector3(x, -0.1f, z+CellHeight/2)+Wall.transform.position+Util.wallOffset, Quaternion.Euler(0,0,0)) 
                        as GameObject;// front
					tmp.transform.parent = transform;
                    tmp.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(colorRange, 0.73f, 0.96f);
                }
				if(cell.WallLeft){
					tmp = Instantiate(Wall,
                        new Vector3(x-CellWidth/2, -0.1f, z)+Wall.transform.position + Util.wallOffset, Quaternion.Euler(0,270,0)) 
                        as GameObject;// left
					tmp.transform.parent = transform;
                    tmp.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(colorRange, 0.73f, 0.96f);
                }
				if(cell.WallBack){
					tmp = Instantiate(Wall,
                        new Vector3(x, -0.1f, z-CellHeight/2)+Wall.transform.position + Util.wallOffset, Quaternion.Euler(0,180,0)) 
                        as GameObject;// back
					tmp.transform.parent = transform;
                    tmp.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(colorRange, 0.73f, 0.96f);
                }
			}
		}
    }
}
