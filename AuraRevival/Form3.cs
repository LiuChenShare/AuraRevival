using AuraRevival.Business;
using AuraRevival.Business.Battle;
using AuraRevival.Business.Construct;
using AuraRevival.Business.Entity;
using System.Data;
using System.Drawing;

namespace AuraRevival
{
    public partial class Form3 : Form
    {
        /// <summary>
        /// 上次点击的坐标
        /// </summary>
        public Coor CoorOld { get; set; } = new Coor(0, 0);
        /// <summary>
        /// 界面左上角的格子
        /// </summary>
        public Coor ZeroCoor { get; set; } = new Coor(0, 0);

        public Form3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 页面加载结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_Shown(object sender, EventArgs e)
        {
            panel_Map.Size = new Size(Grain.Instance.MainGame.MapSize.Item1 * 25 + 1, Grain.Instance.MainGame.MapSize.Item2 * 25 + 1);
            button8.BackgroundImage = Image.FromFile(Util.房子_灰);
            button8.BackgroundImageLayout = ImageLayout.Zoom;
            GoHome();

            listView1.LabelWrap = true;
            listView1.Columns.Add(new ColumnHeader()
            {
                Name = "ColumnHeader1",
                Text = "消息",
                Width = listView1.Width,
            });
            //listView1.Scrollable
            //FormRefresh();
            MainGame.Instance.GameStart();
            MainGame.Instance.MsgEvent += ShowMsg;
            MainGame.Instance.SecondsEvent += ShowDate;
            MainGame.Instance.EntityMoveEvent += EntityMoveEvent;
            MainGame.Instance.BlockUpdateEvent += BlockUpdateEvent;
        }

        /// <summary>
        /// 实体更新
        /// </summary>
        /// <param name="blockId"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void BlockUpdateEvent(Point blockId)
        {
            FormRefresh();
        }

        /// <summary>
        /// 当实体移动时发生
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="L_old"></param>
        /// <param name="L_new"></param>
        private void EntityMoveEvent(IEntity entity, Point? L_old, Point? L_new)
        {
            FormRefresh();
        }


        /// <summary>
        /// 当鼠标指针位于控件上并按下鼠标键时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_Map_MouseDown(object sender, MouseEventArgs e)
        {
            int padding = Util.Padding;
            Coor coor = new Coor(e.X / padding, e.Y / padding);

            #region 上次点击的位置标绿
            Graphics g2 = panel_Map.CreateGraphics();
            Pen pen2 = new Pen(Color.Green);

            g2.DrawRectangle(pen2, CoorOld.Rectangle);
            g2.Dispose();
            #endregion

            #region 点击的位置标红
            Graphics g = panel_Map.CreateGraphics();
            Pen pen = new Pen(Color.Red);

            ////两横
            //g.DrawLine(pen, coor.Rectangle.Location, new Point(coor.Rectangle.Location.X + 1, coor.Rectangle.Location.Y));
            //g.DrawLine(pen, new Point(coor.Rectangle.Location.X, coor.Rectangle.Location.Y + 1), new Point(coor.Rectangle.Location.X + 1, coor.Rectangle.Location.Y + 1));
            ////两竖
            //g.DrawLine(pen, coor.Rectangle.Location, new Point(coor.Rectangle.Location.X, coor.Rectangle.Location.Y + 1));
            //g.DrawLine(pen, new Point(coor.Rectangle.Location.X + 1, coor.Rectangle.Location.Y), new Point(coor.Rectangle.Location.X + 1, coor.Rectangle.Location.Y + 1));

            //废弃上面的方法，直接画矩形
            g.DrawRectangle(pen, coor.Rectangle);
            g.Dispose();
            #endregion

            CoorOld = coor;
            ShowInfo(coor.CoorPoint);
        }

        /// <summary>
        /// 向左
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            int padding = Util.Padding;
            panel_Map.Location = new Point(panel_Map.Location.X + padding, panel_Map.Location.Y);
            ButtonRefresh();
        }

        /// <summary>
        /// 向右
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            int padding = Util.Padding;
            panel_Map.Location = new Point(panel_Map.Location.X - padding, panel_Map.Location.Y);
            ButtonRefresh();
        }

        /// <summary>
        /// 向上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            int padding = Util.Padding;
            panel_Map.Location = new Point(panel_Map.Location.X, panel_Map.Location.Y + padding);
            ButtonRefresh();
        }

        /// <summary>
        /// 向下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            int padding = Util.Padding;
            panel_Map.Location = new Point(panel_Map.Location.X, panel_Map.Location.Y - padding);
            ButtonRefresh();
        }

        /// <summary>
        /// 回到基地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            GoHome();
            ButtonRefresh();
        }

        /// <summary>
        /// 地图刷新
        /// </summary>
        private void FormRefresh()
        {
            Graphics g = panel_Map.CreateGraphics();
            Pen pen = new Pen(Color.Green);
            Pen penRed = new Pen(Color.Red);
            Coor coor111111 = new Coor(1, 1);

            //框定展示的范围
            Rectangle rec = new Rectangle(-panel_Map.Location.X, -panel_Map.Location.Y, panel_MapView.Width, panel_MapView.Height);
            int maxHeight = rec.Y + rec.Height;
            int maxWidth = rec.X + rec.Width;

            //循环绘制多条横线
            for (int i = rec.Y; i <= maxHeight; i += 25)
            {
                g.DrawLine(pen, rec.X, i, maxWidth, i);
            }
            //循环绘制多条竖线
            for (int i = rec.X; i <= maxWidth; i += 25)
            {
                g.DrawLine(pen, i, rec.Y, i, maxHeight);
            }

            //在地图(1,1)的位置放个图片
            g.DrawImage(Image.FromFile(@".\Image\雪景.png"), coor111111.Rectangle.X, coor111111.Rectangle.Y, coor111111.Rectangle.Width, coor111111.Rectangle.Height);

            //当前选中的地方画矩形
            g.DrawRectangle(penRed, CoorOld.Rectangle);



            #region 绘制建筑(废弃)
            ////计算界面能展示多少格子
            //int viewXMax = panel_MapView.Width / Util.Padding;
            //int viewYMax = panel_MapView.Height / Util.Padding;
            ////获取界面展示范围
            //Rectangle rectangle = new Rectangle(ZeroCoor.CoorPoint, new Size(viewXMax, viewYMax));
            //List<IConstruct> constructs = Grain.Instance.Constructs.Where(x => rectangle.Contains(x.Location)).ToList();
            //foreach (IConstruct construct in constructs)
            //{
            //    //获得基地在界面的坐标
            //    Coor constructCoor = new(construct.Location);
            //    string imagePaht = construct.Type switch
            //    {
            //        ConstructType.Default => Util.房子_灰,
            //        ConstructType.Base => Util.房子_蓝,
            //        _ => Util.房子_灰,
            //    };
            //    g.DrawImage(Image.FromFile(imagePaht),
            //        constructCoor.Rectangle.X,
            //        constructCoor.Rectangle.Y,
            //        constructCoor.Rectangle.Width,
            //        constructCoor.Rectangle.Height);
            //}
            #endregion

            #region 绘制展示的区块
            //计算界面能展示多少格子
            int viewXMax = panel_MapView.Width / Util.Padding;
            int viewYMax = panel_MapView.Height / Util.Padding;
            //获取界面展示范围
            Rectangle rectangle = new Rectangle(ZeroCoor.CoorPoint, new Size(viewXMax, viewYMax));
            List<Block> blocks = Grain.Instance.Blocks.Where(x => rectangle.Contains(x.Id)).ToList();
            foreach (Block block in blocks)
            {
                //获得区块在界面的坐标
                Coor constructCoor = new(block.Id);
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(constructCoor.Rectangle.X + 1, constructCoor.Rectangle.Y + 1, constructCoor.Rectangle.Width - 1, constructCoor.Rectangle.Height - 1));


                #region 绘制建筑
                if (block.Constructs.Any())
                {
                    IConstruct construct = block.Constructs.OrderBy(x => x.Type).FirstOrDefault();

                    string imagePaht = construct.Type switch
                    {
                        ConstructType.Default => Util.房子_灰,
                        ConstructType.Base => Util.房子_蓝,
                        _ => Util.房子_灰,
                    };
                    g.DrawImage(Image.FromFile(imagePaht),
                        constructCoor.Rectangle.X,
                        constructCoor.Rectangle.Y,
                        constructCoor.Rectangle.Width,
                        constructCoor.Rectangle.Height);
                }
                #endregion

                #region 绘制实体
                if (block.Entities.Any())
                {
                    IEntity entity = block.Entities.OrderBy(x => x.Type).FirstOrDefault();

                    string imagePath = entity.Type switch
                    {
                        0 => Util.士兵,
                        2 => Util.小怪,
                        _ => Util.士兵,
                    };
                    g.DrawImage(Image.FromFile(imagePath),
                        constructCoor.Rectangle.X,
                        constructCoor.Rectangle.Y,
                        constructCoor.Rectangle.Width,
                        constructCoor.Rectangle.Height);
                }
                #endregion

                #region 绘制战斗图标
                var aaa = block.BattleRooms.LastOrDefault(x => x.State == BattleStateType.InBattle || x.State == BattleStateType.BattleOver);
                if (aaa != null)
                {
                    string imagePath = aaa.State switch
                    {
                        BattleStateType.InBattle => Util.对战_红,
                        BattleStateType.BattleOver => Util.对战_黑,
                        _ => Util.对战_黑,
                    };
                    g.DrawImage(Image.FromFile(imagePath),
                        constructCoor.Rectangle.X,
                        constructCoor.Rectangle.Y,
                        constructCoor.Rectangle.Width,
                        constructCoor.Rectangle.Height);
                }
                #endregion
            }
            #endregion


            g.Dispose();

            ButtonRefresh();
        }

        /// <summary>
        /// 刷新按钮
        /// </summary>
        private void ButtonRefresh()
        {
            int padding = Util.Padding;
            button1.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;

            if (panel_Map.Location.X > (-padding))
            {
                button1.Enabled = false;
            }
            if (panel_Map.Location.X < (panel_MapView.Width - panel_Map.Width - padding))
            {
                button5.Enabled = false;
            }
            if (panel_Map.Location.Y > (-padding))
            {
                button6.Enabled = false;
            }
            if (panel_Map.Location.Y < (panel_MapView.Height - panel_Map.Height - padding))
            {
                button7.Enabled = false;
            }
        }

        /// <summary>
        /// 地图重绘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormRefresh();
        }

        /// <summary>
        /// 在鼠标指针在地图上上并释放鼠标键时发生
        /// 初始化右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_Map_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                //初始化右键菜单
                contextMenuStrip1.Items.Clear();
                ToolStripMenuItem menuItem_Title = new()
                {
                    Enabled = false,
                };
                List<ToolStripItem> stripItemsConstruct = new List<ToolStripItem>();//建筑右键菜单


                //判断当前是哪个区块
                int padding = Util.Padding;
                Coor coor = new Coor(e.X / padding, e.Y / padding);
                menuItem_Title.Text = $"区块（{coor.CoorPoint.X},{coor.CoorPoint.Y}）";

                if (!Grain.Instance.Blocks.Any(x => x.Id == coor.CoorPoint))
                    menuItem_Title.Text = $"{menuItem_Title.Text}（未探索）";
                else
                {
                    menuItem_Title.ForeColor = Color.Black;
                    Block block = Grain.Instance.Blocks.FirstOrDefault(x => x.Id == coor.CoorPoint);
                    //加入建筑右键菜单
                    if (block.Constructs.Any())
                    {
                        foreach (var construct in block.Constructs)
                        {
                            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                            toolStripMenuItem.Text = construct.Name;
                            if (construct.Type == ConstructType.Base)
                            {
                                ToolStripMenuItem stripMenu_UpLevel = new ToolStripMenuItem();
                                stripMenu_UpLevel.Text = "升级";
                                stripMenu_UpLevel.Click += toolStripMenuItem_ConstructClick;
                                stripMenu_UpLevel.Tag = new Tuple<int, object, IConstruct>(1, null, construct);
                                toolStripMenuItem.DropDownItems.Add(stripMenu_UpLevel);
                            }
                            stripItemsConstruct.Add(toolStripMenuItem);
                        }
                        stripItemsConstruct.Add(new ToolStripSeparator());
                    }

                    //加入实体右键菜单
                    if (block.Entities.Any())
                    {
                        foreach (var entity in block.Entities)
                        {
                            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                            toolStripMenuItem.Text = entity.Name;

                            ToolStripMenuItem stripMenu_Move = new ToolStripMenuItem();
                            stripMenu_Move.Text = "移动";

                            //上
                            ToolStripMenuItem stripMenu_MoveUp = new ToolStripMenuItem();
                            stripMenu_MoveUp.Text = "向上";
                            stripMenu_MoveUp.Click += toolStripMenuItem_EntityClick;
                            stripMenu_MoveUp.Tag = new Tuple<int, object, IEntity>((int)ScriptComd.Entity_Move, "W", entity);
                            //下
                            ToolStripMenuItem stripMenu_MoveDown = new ToolStripMenuItem();
                            stripMenu_MoveDown.Text = "向下";
                            stripMenu_MoveDown.Click += toolStripMenuItem_EntityClick;
                            stripMenu_MoveDown.Tag = new Tuple<int, object, IEntity>((int)ScriptComd.Entity_Move, "S", entity);
                            //左
                            ToolStripMenuItem stripMenu_MoveLeft = new ToolStripMenuItem();
                            stripMenu_MoveLeft.Text = "向左";
                            stripMenu_MoveLeft.Click += toolStripMenuItem_EntityClick;
                            stripMenu_MoveLeft.Tag = new Tuple<int, object, IEntity>((int)ScriptComd.Entity_Move, "A", entity);
                            //右
                            ToolStripMenuItem stripMenu_MoveRight = new ToolStripMenuItem();
                            stripMenu_MoveRight.Text = "向右";
                            stripMenu_MoveRight.Click += toolStripMenuItem_EntityClick;
                            stripMenu_MoveRight.Tag = new Tuple<int, object, IEntity>((int)ScriptComd.Entity_Move, "D", entity);

                            stripMenu_Move.DropDownItems.Add(stripMenu_MoveUp);
                            stripMenu_Move.DropDownItems.Add(stripMenu_MoveDown);
                            stripMenu_Move.DropDownItems.Add(stripMenu_MoveLeft);
                            stripMenu_Move.DropDownItems.Add(stripMenu_MoveRight);
                            toolStripMenuItem.DropDownItems.Add(stripMenu_Move);
                            
                            stripItemsConstruct.Add(toolStripMenuItem);
                        }
                        stripItemsConstruct.Add(new ToolStripSeparator());
                    }
                }

                contextMenuStrip1.Items.AddRange(new ToolStripItem[] { menuItem_Title, new ToolStripSeparator() });
                contextMenuStrip1.Items.AddRange(stripItemsConstruct.ToArray());

                //contextMenuStrip1.Show(this, e.Location);
                //contextMenuStrip1.Show(this,5,5);
                contextMenuStrip1.Show((Control)sender, e.Location);
            }
        }


        /// <summary>
        /// 展示消息
        /// </summary>
        /// <param name="type">来源类型:0-游戏公告，1-区块，2-建筑，3-实体</param>
        /// <param name="source">来源名称</param>
        /// <param name="content">消息内容</param>
        private void ShowMsg(int type, string source, string content)
        {
            listView1.Invoke(new Action(() =>
            {
                if (listView1.Items.Count > 100)
                {
                    listView1.Items.RemoveAt(0);
                }
                ListViewItem item = new ListViewItem();
                switch (type)
                {
                    case 0:
                        item.Text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - *{source}*：{content}";
                        item.ForeColor = Color.Red;
                        break;
                    case 1:
                        item.Text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - ({source})：{content}";
                        item.ForeColor = Color.Brown;
                        break;
                    case 2:
                        item.Text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - 【{source}】：{content}";
                        item.ForeColor = Color.Blue;
                        break;
                    case 3:
                        item.Text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - [{source}]：{content}";
                        item.ForeColor = Color.Black;
                        break;
                    default:
                        item.Text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - [{source}]：{content}";
                        item.ForeColor = Color.Gray;
                        break;
                }
                listView1.Items.Add(item);

                listView1.Focus(); //聚焦光标
                //listView1.Items[listView1.Items.Count - 1].Selected = true; //选中最后一行
                listView1.Items[listView1.Items.Count - 1].EnsureVisible(); ;//显示内容自动滚动到最后一行
            }));
        }

        /// <summary>
        /// 展示地图区块信息
        /// </summary>
        /// <param name="point"></param>
        private void ShowInfo(Point point)
        {

            Thread thread = new Thread(() =>
            {

                List<ListViewItem> lvs = new List<ListViewItem>();
                List<ListViewGroup> groups = new List<ListViewGroup>();
                ListViewGroup group_Block = new ListViewGroup
                {
                    Header = "区块",
                    //Footer = "区块啊"
                };
                ListViewGroup group_Construct = new ListViewGroup
                {
                    Header = "建筑",
                    //Footer = "建筑啊"
                };
                ListViewGroup group_Entitie = new ListViewGroup
                {
                    Header = "实体",
                    //Footer = "实体啊"
                };

                var block = Grain.Instance.Blocks.FirstOrDefault(x => x.Id == point);
                if (block == null)
                {
                    ListViewItem lvBlock = new ListViewItem("区块");
                    lvBlock.Group = group_Block;
                    lvBlock.SubItems.Add("未探索");
                    lvs.Add(lvBlock);
                }
                else
                {
                    ListViewItem lvBlock = new ListViewItem("区块");
                    lvBlock.Group = group_Block;
                    lvBlock.SubItems.Add($"（{block.Id.X},{block.Id.Y}）");
                    lvs.Add(lvBlock);
                    foreach (var item in block.Constructs)
                    {
                        ListViewItem lvConstruct = new ListViewItem(item.Name);
                        lvConstruct.Group = group_Construct;
                        lvConstruct.SubItems.Add($"等级：{item.Level}；{item.Description}");
                        lvs.Add(lvConstruct);
                    }
                    foreach (var item in block.Entities)
                    {
                        ListViewItem lvConstruct = new ListViewItem(item.Name);
                        lvConstruct.Group = group_Entitie;
                        lvConstruct.SubItems.Add($"等级：{item.Level}；{item.Description}");
                        lvs.Add(lvConstruct);
                    }
                }


                listView2.Invoke(new Action(() =>
                {
                    listView2.BeginUpdate();
                    listView2.Items.Clear();
                    listView2.Groups.Clear();
                    listView2.Groups.AddRange(new ListViewGroup[] { group_Block, group_Construct,group_Entitie });
                    listView2.Items.AddRange(lvs.ToArray());

                    listView2.EndUpdate();
                }));

                return;
            });
            thread.Start();
        }

        /// <summary>
        /// 展示时间
        /// </summary>
        /// <param name="time"></param>
        private async Task ShowDate(DateTime time)
        {
            label1.Invoke(new Action(() =>
            {
                label1.Text = time.ToString("yyyy-MM-dd HH:mm:ss");
            }));
        }

        /// <summary>
        /// 地图跳转至基地位置
        /// </summary>
        private void GoHome()
        {
            AuraRevival.Business.Construct.Construct_Base construct = Grain.Instance.GetConstructBase();
            //获得基地在界面的坐标
            Coor constructCoor = new(construct.Location);
            //计算界面能展示多少格子
            int viewXMax = panel_MapView.Width / Util.Padding;
            int viewYMax = panel_MapView.Height / Util.Padding;
            //获得地图的偏移坐标
            int x = construct.Location.X - viewXMax / 2;
            int y = construct.Location.Y - viewYMax / 2;
            //获得应该放在界面左上角的格子信息
            ZeroCoor = new(x, y);
            //移动地图，使基地在中间
            panel_Map.Location = new Point(-ZeroCoor.Rectangle.X, -ZeroCoor.Rectangle.Y);
            ////基地绘图
            //Graphics g = panel_Map.CreateGraphics();
            //g.DrawImage(Image.FromFile(Util.房子_蓝), constructCoor.Rectangle.X, constructCoor.Rectangle.Y, constructCoor.Rectangle.Width, constructCoor.Rectangle.Height);
            //g.Dispose();
        }

        /// <summary>
        /// 建筑右键菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_ConstructClick(object? sender, EventArgs e)
        {
            ToolStripItem? stripItem = sender as ToolStripItem;
            if (stripItem?.Tag == null) return;
            Tuple<int, object, IConstruct> tag = stripItem.Tag as Tuple<int, object, IConstruct>;

            if(!tag.Item3.ScriptEvent(tag.Item1, tag.Item2))
            {
                MessageBox.Show( $"{stripItem.Text}操作失败", tag.Item3.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 实体右键菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_EntityClick(object? sender, EventArgs e)
        {
            ToolStripItem? stripItem = sender as ToolStripItem;
            if (stripItem?.Tag == null) return;
            Tuple<int, object, IEntity> tag = stripItem.Tag as Tuple<int, object, IEntity>;

            if (!tag.Item3.ScriptEvent(tag.Item1, tag.Item2))
            {
                MessageBox.Show($"{stripItem.Text}操作失败", tag.Item3.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var block = Grain.Instance.Blocks.FirstOrDefault(x => x.Id == CoorOld.CoorPoint);
            block.AddEnemy();
        }
    }
}
