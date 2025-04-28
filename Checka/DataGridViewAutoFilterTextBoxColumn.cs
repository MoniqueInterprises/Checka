using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

public class DataGridViewAutoFilterTextBoxColumn : DataGridViewTextBoxColumn
{
    public DataGridViewAutoFilterTextBoxColumn()
    {
        this.CellTemplate = new DataGridViewAutoFilterTextBoxCell();
    }
}

public class DataGridViewAutoFilterTextBoxCell : DataGridViewTextBoxCell
{
    protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
    {
        base.OnMouseClick(e);

        if (this.RowIndex == -1 && e.Button == MouseButtons.Left)
        {
            var grid = this.DataGridView;
            var colIndex = this.ColumnIndex;

            if (grid.DataSource is BindingSource bs && bs.DataSource is DataTable dt)
            {
                string coluna = grid.Columns[colIndex].DataPropertyName;

                var valores = dt.AsEnumerable()
                                .Select(r => r[coluna]?.ToString())
                                .Distinct()
                                .OrderBy(v => v)
                                .ToList();

                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("Todos", null, (s, a) => bs.RemoveFilter());

                foreach (var val in valores)
                {
                    if (val != null)
                    {
                        var item = new ToolStripMenuItem(val);
                        item.Click += (s, a) =>
                        {
                            bs.Filter = $"[{coluna}] = '{val.Replace("'", "''")}'";
                        };
                        menu.Items.Add(item);
                    }
                }

                var cellDisplayRect = grid.GetCellDisplayRectangle(colIndex, -1, true);
                var pt = new System.Drawing.Point(cellDisplayRect.Left, cellDisplayRect.Bottom);
                menu.Show(grid, pt);
            }
        }
    }
}
