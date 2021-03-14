using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Stanok.ViewModel;

namespace Stanok
{
    /// <summary>
    /// Логика взаимодействия для VisualComponent.xaml
    /// </summary>
    public partial class VisualComponent : UserControl
    {
        public VisualComponent()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (DataContext is MainViewModel viewModel)
            {
                RenderWood(viewModel);
            }
        }

        public GeometryModel3D CreateTriangle(Point3D p0, Point3D p1, Point3D p2)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            
            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.Brown));
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            return model;
        }

        public Model3DGroup CreateCube(double x, double y, double z, double A = 1, double B = 1, double C = 1)
        {
            Model3DGroup cube = new Model3DGroup();

            Point3D p0 = new Point3D(0 + x, 0 + y, 0 + z);
            Point3D p1 = new Point3D(A + x, 0 + y, 0 + z);
            Point3D p2 = new Point3D(A + x, 0 + y, C + z);
            Point3D p3 = new Point3D(0 + x, 0 + y, C + z);
            Point3D p4 = new Point3D(0 + x, B + y, 0 + z);
            Point3D p5 = new Point3D(A + x, B + y, 0 + z);
            Point3D p6 = new Point3D(A + x, B + y, C + z);
            Point3D p7 = new Point3D(0 + x, B + y, C + z);

            // top
            cube.Children.Add(CreateTriangle(p3, p2, p6));
            cube.Children.Add(CreateTriangle(p3, p6, p7));

            // right
            cube.Children.Add(CreateTriangle(p2, p1, p5));
            cube.Children.Add(CreateTriangle(p2, p5, p6));

            // bottom
            //cube.Children.Add(CreateTriangle(p1, p0, p4));
            //cube.Children.Add(CreateTriangle(p1, p4, p5));

            // left
            cube.Children.Add(CreateTriangle(p0, p3, p7));
            cube.Children.Add(CreateTriangle(p0, p7, p4));

            // back
            cube.Children.Add(CreateTriangle(p7, p6, p5));
            cube.Children.Add(CreateTriangle(p7, p5, p4));

            // front
            cube.Children.Add(CreateTriangle(p2, p3, p0));
            cube.Children.Add(CreateTriangle(p2, p0, p1));

            return cube;

        }
        public Model3DGroup CreateCube(Point3D point, Size3D size)
        {
            return CreateCube(point.X, point.Y, point.Z, size.X, size.Y, size.Z);
        }

        public ModelVisual3D CreateVisualCube(double x, double y, double z, double A = 1, double B = 1, double C = 1) {
            ModelVisual3D model = new ModelVisual3D();
            model.Content = CreateCube(x, y, z, A, B, C);
            return model;
        }

        void RenderWood(MainViewModel viewModel)
        {
            int rows = viewModel.Cube.SizeX;
            int columns = viewModel.Cube.SizeY;

            // Создаём блоки
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    var item = viewModel.Cube.Matrix[x,y];
                    var cube = RenderCubeItem(item);

                    // Подписываемся на изменение элемента
                    item.PropertyChanged += (s, e) =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            cubeModelGroup.Children.Remove(cube);
                            cube = RenderCubeItem(s as CubeItemViewModel);
                        });
                    };

                }
            }
        }



        /// <summary>
        /// Размеры одного элемнта бруска (для визуализации)
        /// </summary>
        static Size3D ItemSize = new Size3D(0.2, 0.2, 0.2);

        Model3DGroup RenderCubeItem(CubeItemViewModel item)
        {
            var size = ItemSize;
            var x = item.X; var y = item.Y;

            var k = 1.01; // 1.0 - без промежутков, 1.05 и больше - с промежутками
            var point = new Point3D(k * x * size.X, k * y * size.Y, 0);
            var cubeSize = new Size3D(size.X, size.Y, Math.Max(0, item.Z * size.Z));
            var cube = CreateCube(point, cubeSize);
            cubeModelGroup.Children.Add(cube);
            return cube;
        }
    }
}
