namespace tz
{
    class Pixel
    {
        public int red;
        public int green;
        public int blue;
    }

    class MedianFilter
    {
        public Pixel[][] Filter(Pixel[][]pixels)
        {
            Pixel[][] result = new Pixel[pixels.Length][];
            for (int i = 0; i < pixels.Length; i++)
            {
                result[i] = new Pixel[pixels[0].Length];
            }

            for (int i = 0; i < pixels.Length; i++)
            {
                for (int j = 0; j < pixels[i].Length; j++)
                {
                    if(i == 0 || j == 0 || i ==pixels.Length - 1 || j == pixels[0].Length -1)
                    {
                        result[i][j] = pixels[i][j];
                        continue;
                    }



                    List<Pixel> neighbors = new List<Pixel>();

                    for (int k = i-1; k < i + 2; k++)
                    {
                        for (int l = j - 1; l < j + 2; l++)
                        {
                            neighbors.Add(pixels[k][l]);
                        }
                    }

                    neighbors.Sort((a, b) => (a.red + a.green + a.blue).CompareTo(b.red + b.green + b.blue));
                    Pixel median = neighbors[4];
                    result[i][j] = median;
                }
            }
            return result;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            int height = 5;
            int width = 5;

            Pixel[][] image = new Pixel[height][];
            Random rand = new Random();

            for (int i = 0; i < height; i++)
            {
                image[i] = new Pixel[width];
                for (int j = 0; j < width; j++)
                {
                    image[i][j] = new Pixel
                    {
                        red = rand.Next(256),
                        green = rand.Next(256),
                        blue = rand.Next(256)
                    };
                }
            }

            Console.WriteLine("Original Image:");
            PrintImage(image);

            MedianFilter filter = new MedianFilter();
            Pixel[][] filtered = filter.Filter(image);

            Console.WriteLine("\nFiltered Image:");
            PrintImage(filtered);
        }

        static void PrintImage(Pixel[][] image)
        {
            for (int i = 0; i < image.Length; i++)
            {
                for (int j = 0; j < image[i].Length; j++)
                {
                    Pixel p = image[i][j];
                    Console.Write($"({p.red:D3},{p.green:D3},{p.blue:D3}) ");
                }
                Console.WriteLine();
            }
        }
    }

}