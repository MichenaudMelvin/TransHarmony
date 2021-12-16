using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour{

    public List<string> musicStyles;
    public List<Color> musicColors;

    public float satisfactionPoints;

    public bool isFinished;

    public List<string> finalMusicList;
    public int correctMusicStyles;

    public Color mixedColor;

    public int index0;
    public int index1;
    public int index2;
    public int index3;

    public void changePrefrenceColor(RawImage rawImage, int index){
        rawImage.color = musicColors[index];
        Color currColor = rawImage.color;
        currColor.a = 1f;
        rawImage.color = currColor;
    }

    public void changePrefrenceColor(RawImage rawImage, int index0, int index1){
        // pas clair au niveau de la couleur
        rawImage.color = musicColors[index0] + ((musicColors[index1] - musicColors[index0])/2);
        Color currColor = rawImage.color;
        currColor.a = 1f;
        rawImage.color = currColor;
    }

    public void ManagePoints(List<ArtistsAttributes> artistList, List<string> publicPreferences, RawImage preferences){
        for(int i = 0; i < artistList.Count; i++){
            // check tous les artistes qui sont dans un hall
            // et check si la contrainte de l'artiste est valide
            if(artistList[i].GetComponent<DragNDrop>().inHall){
                if(artistList[i].validConstraint){
                    satisfactionPoints += artistList[i].rewardPoints * artistList[i].pointMultiplier;
                } else{
                    satisfactionPoints += artistList[i].rewardPoints;
                }

                if(index0 == -1){index0 = i;}
                else if(index1 == -1){index1 = i;}
                else if(index2 == -1){index2 = i;}
                else if(index3 == -1){index3 = i;}

                finalMusicList.Add(artistList[i].preferencesName);
            }
        }


        // à faire : calcul des points du public
        // pour l'instant ça
        for(int i = 0; i < publicPreferences.Count; i++){
            for(int j = 0; j < finalMusicList.Count; j++){
                if(publicPreferences[i] == finalMusicList[j]){
                    correctMusicStyles += 1;
                }
            }
        }

        if(correctMusicStyles > 0){
            satisfactionPoints *= correctMusicStyles;
        }

        // mixedColor = ((artistList[index1].preferences.color - artistList[index0].preferences.color)/2) + ((artistList[index3].preferences.color - artistList[index2].preferences.color)/2);

        // // téma l'enfer, et c'est meme pas bon
        // RGB data = new RGB((int)Mathf.Round(Mathf.Abs(mixedColor.r * 255)), (int)Mathf.Round(Mathf.Abs(mixedColor.g * 255)), (int)Mathf.Round(Mathf.Abs(mixedColor.b * 255)));
        // string decimalMixedColor = RGBToHexadecimal(data);
        // RGB data2 = new RGB((int)Mathf.Round(Mathf.Abs(preferences.color.r * 255)), (int)Mathf.Round(Mathf.Abs(preferences.color.g * 255)), (int)Mathf.Round(Mathf.Abs(preferences.color.b * 255)));
        // string decimalPreferences = RGBToHexadecimal(data2);

        // int newDecimalMixedColor = int.Parse(decimalMixedColor, System.Globalization.NumberStyles.HexNumber);
        // int newDecimalPreferences = int.Parse(decimalPreferences, System.Globalization.NumberStyles.HexNumber);

        // print(newDecimalMixedColor);
        // print(newDecimalPreferences);
        // if(newDecimalMixedColor > newDecimalPreferences){
        //     print(((newDecimalPreferences - newDecimalMixedColor) / newDecimalPreferences) * 100);
        // } else {
        //     print(((newDecimalMixedColor - newDecimalPreferences) / newDecimalMixedColor) * 100);
        // }
    }

    // pris d'ici : https://www.programmingalgorithms.com/algorithm/rgb-to-hexadecimal/
    public struct RGB{
        private int _r;
        private int _g;
        private int _b;

        public RGB(int r, int g, int b){
            this._r = r;
            this._g = g;
            this._b = b;
        }

        public int R{
            get { return this._r; }
            set { this._r = value; }
        }

        public int G{
            get { return this._g; }
            set { this._g = value; }
        }

        public int B{
            get { return this._b; }
            set { this._b = value; }
        }

        public bool Equals(RGB rgb){
            return (this.R == rgb.R) && (this.G == rgb.G) && (this.B == rgb.B);
        }
    }

    public static string RGBToHexadecimal(RGB rgb){
        string rs = DecimalToHexadecimal(rgb.R);
        string gs = DecimalToHexadecimal(rgb.G);
        string bs = DecimalToHexadecimal(rgb.B);

        return rs + gs + bs;
    }

    private static string DecimalToHexadecimal(int dec){
        if (dec <= 0)
            return "00";

        int hex = dec;
        string hexStr = string.Empty;

        while (dec > 0)
        {
            hex = dec % 16;

            if (hex < 10)
                hexStr = hexStr.Insert(0, System.Convert.ToChar(hex + 48).ToString());
            else
                hexStr = hexStr.Insert(0, System.Convert.ToChar(hex + 55).ToString());

            dec /= 16;
        }

        return hexStr;
    }
}
