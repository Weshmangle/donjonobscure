using System.Collections;
using System.Collections.Generic;


class GFG
{	
	public static List<(int, int)> MidPointCircleDraw(int x_centre, int y_centre, int r)
	{
        List<(int, int)> coordinates = new List<(int, int)>();
		int x = r, y = 0;
	
        coordinates.Add(((x + x_centre), (y + y_centre)));
	
		if (r > 0)
		{
			coordinates.Add(((-x + x_centre), (-y + y_centre)));
            coordinates.Add(((y + x_centre), (x + y_centre)));
            coordinates.Add(((-y + x_centre), (-x + y_centre)));
		}
	
		int P = 1 - r;
		while (x > y)
		{
			y++;
		
			if (P <= 0)
				P = P + 2 * y + 1;
			else
			{
				x--;
				P = P + 2 * y - 2 * x + 1;
			}
		
			if (x < y)
				break;
            
            coordinates.Add(((x + x_centre), (y + y_centre)));
            
			coordinates.Add(((-x + x_centre), (y + y_centre)));
			
            coordinates.Add(((x + x_centre), (-y + y_centre)));
            
            coordinates.Add(((-x + x_centre), (-y + y_centre)));
            
			if (x != y)
			{
                coordinates.Add(((y + x_centre), (x + y_centre)));
                
                coordinates.Add(((-y + x_centre), (x + y_centre)));
                
                coordinates.Add(((y + x_centre), (-x + y_centre)));
                
                coordinates.Add(((-y + x_centre), (-x + y_centre)));
			}
		}

        return coordinates;
	}
}
