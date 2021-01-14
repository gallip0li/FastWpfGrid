using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FastWpfGrid;

namespace FastWpfGridTest
{
    public class GridModel2 : FastGridModelBase
    {
        public override int ColumnCount
        {
            get { return 5; }
        }

        public override int RowCount
        {
            get { return 100; }
        }

        public override IFastGridCell GetColumnHeader(IFastGridView view, int column)
        {
	        var primaryKeyImg = GridModelFunctions.PathFromOutputDir("primary_keysmall.png", "Images");
	        var foreignKeyImg = GridModelFunctions.PathFromOutputDir("foreign_keysmall.png", "Images");
            string image = null;
            
            if (column == 0)
	            image = primaryKeyImg;

            if (column == 2)
	            image = foreignKeyImg;

            var res = new FastGridCellImpl();
            if (image != null)
            {
                res.Blocks.Add(new FastGridBlockImpl
                    {
                        BlockType = FastGridBlockType.Image,
                        ImageWidth = 16,
                        ImageHeight = 16,
                        ImageSource = image,
                    });
            }

            res.Blocks.Add(new FastGridBlockImpl
                {
                    IsBold = true,
                    TextData = String.Format("Column {0}", column),
                });

            var btn = res.AddImageBlock(foreignKeyImg);
            btn.MouseHoverBehaviour = MouseHoverBehaviours.HideWhenMouseOut;
            btn.CommandParameter = "TEST";


            return res;
        }

        public override IFastGridCell GetCell(IFastGridView view, int row, int column)
        {
            if ((row+column) % 4 == 0)
            {
                return new FastGridCellImpl
                {
                    SetBlocks = new[]
                        {
                            new FastGridBlockImpl
                                {
                                    IsItalic = true,
                                    FontColor = Colors.Gray,
                                    TextData = "(NULL)",
                                }
                        },
                        Decoration = CellDecoration.StrikeOutHorizontal,
                        DecorationColor = Colors.Red,
                };
            }

            var foreignKeyImg = GridModelFunctions.PathFromOutputDir("foreign_keysmall.png", "Images");
            var impl = new FastGridCellImpl();

            impl.AddTextBlock(GetCellText(row, column));
            var btn = impl.AddImageBlock(foreignKeyImg);
            btn.MouseHoverBehaviour = MouseHoverBehaviours.HideWhenMouseOut;

            btn.CommandParameter = "TEST";
            impl.RightAlignBlockCount = 0;
            return impl;
        }

        public override void HandleCommand(IFastGridView view, FastGridCellAddress address, object commandParameter, ref bool handled)
        {
            base.HandleCommand(view, address, commandParameter, ref handled);
        }

        public override string GetCellText(int row, int column)
        {
            return String.Format("{0}{1}", row + 1, column + 1);
        }
    }
}
