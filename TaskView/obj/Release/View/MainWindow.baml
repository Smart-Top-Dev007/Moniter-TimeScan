   M S B A M L   `   `   `  ���� K  GMonitor.TaskView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Monitor.TaskView.ViewMonitor.TaskView.View  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf E'clr-namespace:Monitor.TaskView.ResourceMonitor.TaskView.Resource  &     Monitor.TaskView.View.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:Monitor.TaskView.View     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     <controls'clr-namespace:Monitor.TaskView.Resource     -   $	��770��$
��1200��  � 
ResizeMode$  	CanResize=�$
��1200��$	��770�� � WindowStartupLocation$ CenterScreen=�
���   �.��� ���	#FF363636 � RowDefinitions �� $��50 ��� $��73 ��� $
��29.5 ��� $	��7.5 ��� $��40 ��� $��40 ��� $��40 ��� $
��Auto ��� .+�� -   $��0��$��0��$��50��$	��Top=����	#FF40A6D1 J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�   ShadowDepth$ 3Q�   	Direction$	 270Q�   Opacity$	 0.2Q� � ColumnDefinitions �� $��5 ��� $	��111 ��� $
��224* ��� $
��233* ��� $	��162 �.+�0   *Monitor.TaskView.Resource.HeaderDecoration  $��0q�$��3��$
��Left=�$	��131��$
��Auto��]� $��3��$��0��$
��Left=���.  $��0,5,0,-5q�$	��210��.+��� -   	 ��Name$	 AVARTA��$��25��$��25��$��	10,0,0,12q�$��/Resource/net-off.png��$��Uniform=�
 J� RenderTransformOrigin$
 0.396,1.005&�$��Bottom=��� -    ��Name$ Title��$��Task View 1.3.2��$��Center=�$��Center=�
�������$��SemiBold"�$��22(� J� IsManipulationEnabled . $��0,5,5,5q�$��10,6,0,6q�$	��175���� $��2��$��Center=� �� Orientation$ 
Horizontal=�$��
130,8,24,2q�$��40��.+��� -   $ serverIP��$�� ��$��Bottom=�$��Bottom=�
�������$��SemiBold"�$��18(� . $��5,5,5,5q�$��0,0,0,0q��� $��3��$��Center=�$ 
Horizontal=�$��0,5,0,5q�$��40��$
��Left=�$	��470��$��2��.+�(  "System.Windows.Controls.DatePicker  -     Name$ date_Picker��$��0,12,0,0q�$	��280��$��17(�$��28��$��Right=�   SelectedDateFormat$
 Long=�$��2��|�.  �� -   $	 imgBack��$��/Resource/Back.png��$��25��$��0,16,0,4q�$
 1.302,1.035&��� -   $	 
imgForward��$��/Resource/Forward.png��$��25��$��20��$��4,16,0,4q�$
 12.351,-0.392&�$��2��$��2���� -	    ��Name$ btnToday��   RoundedSettingsButtonStyle#���   ���#ffaa00
�������$��0q�$��60��$��Today��$��20��$��
8,16,505,4q�$
 0.341,0.217&�$��3����Z� �� $��4��$��Center=�$ 
Horizontal=�$��0,10,0,0q�$��40��.+��� -
   $��40��$��40�� � ToolTip$ Setting��$	����  CloseMinMaxButtonStyle#���  
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�$��21(�$��Center=�|�.  �� -   $	����#���  $��40��$��40��$
 Hide��
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�$��20(�$��Bottom=�|�.  �� -   $ 	maxButton��$��40��$��40��$ Resize��|�.  $	����#���  
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�$��20(�$��Bottom=��� -   
������ $��40��$��40��
������ #���  |�.  $ Restart��$��Center=�.���� .+��� $��/Resource/restart.png��$��25��$��25��$
��0, 5q��� $��1��$��1q����	#FFE6E6E6$��
10,7,10,-5q�._�� $��1��$��Stretch=�$��0,0,0,0q� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+�� $��0��.+�]� ���	#EE228B22$��10,7,5,7q�.+��� $
��Left=�$	����$��Center=�$.��(pack://application:,,,/Font/#FontAwesome)�
�������$��Center=�$��35(��� $	��Top=�$��	Work Time��
�������$��0,1,0,0q�$��13(�$��Center=�$��Center=��� -   $��lblTotalTime��$��Bottom=�
�������$
��Bold"�$��0��$��0,-8,0,1q�$��20(�$��Center=�$��Center=�� $��1��.+�]� ���#4682B4$��10,7,5,7q�.+��� $
��Left=�$	����$��Center=�$.��(pack://application:,,,/Font/#FontAwesome)�
�������$��Center=�$��35(��� $	��Top=�$��	Rest Time��$��0,1,0,0q�$��13(�
�������$��Center=�$��Center=��� -   $��lblRestTime��$��Bottom=�
�������$
��Bold"�$��0��$��0,-8,0,1q�$��20(�$��Center=�$��Center=�� $��2��.+�]� ���	#EEFF8C00$��10,7,5,7q�.+��� $
��Left=�$	����$��Center=�$.��(pack://application:,,,/Font/#FontAwesome)�
�������$��Center=�$��35(��� $	��Top=�$��Download��$��0,1,0,0q�$��13(�
�������$��Center=�$��Center=��� -   $��lblDownloadCount��$��Bottom=�
�������$
��Bold"�$��0��$��0,-8,0,1q�$��20(�$��Center=�$��Center=�� $��3��.+�]� ���#3CB371$��10,7,5,7q�.+��� $
��Left=�$	����$��Center=�$.��(pack://application:,,,/Font/#FontAwesome)�
�������$��Center=�$��35(��� $	��Top=�$��Audio��$��0,1,0,0q�$��13(�
�������$��Center=�$��Center=��� -   $��lblAudioCount��$��Bottom=�
�������$
��Bold"�$��0��$��0,-8,0,1q�$��20(�$��Center=�$��Center=�� $��6��.+�]� ���	#DD008B8B$��	10,7,10,7q�.+��� $
��Left=�$	����$��Center=�$.��(pack://application:,,,/Font/#FontAwesome)�
�������$��Center=�$��35(��� $	��Top=�$	��URL��$��0,1,0,0q�$��13(�
�������$��Center=�$��Center=��� -   $��lblURL��$��Bottom=�
�������$
��Bold"�$��0��$��0,-8,0,1q�$��20(�$��Center=�$��Center=�� $��4��.+�]� ���#B22222$��10,7,5,7q�.+��� $
��Left=�$	����$��Center=�$.��(pack://application:,,,/Font/#FontAwesome)�
�������$��Center=�$��35(��� $	��Top=�$��	Forbidden��$��0,1,0,0q�$��13(�
�������$��Center=�$��Center=��� -   $��lblForbiddenCount��$��Bottom=�
�������$
��Bold"�$��0��$��0,-8,0,1q�$��20(�$��Center=�$��Center=�� $��5��.+�]� -    ]�Name$ 	DangerURL��
�����p�$��10,7,5,7q�.+��� $
��Left=�$	����$��Center=�$.��(pack://application:,,,/Font/#FontAwesome)�
�������$��Center=�$��35(��� $	��Top=�$��
Danger URL��$��0,1,0,0q�$��13(�
�������$��Center=�$��Center=��� -   $��lblDangerURLCount��$��Bottom=�
�������$
��Bold"�$��0��$��0,-8,0,1q�$��20(�$��Center=�$��Center=�� $��7��$��Stretch=�$��Stretch=�$��	0,0,0,-33q�$��2��.+�� $��
15,0,15,12q�$
 0.496,0.587&� �� $	��400 ��� $
��165* ��� $��260.5 � �� �� $	��100 ��� $��60 �.+�&   System.Windows.Controls.DataGrid  -     Name$ 	list1View��$��0��$��0��$��3��   
IsReadOnly .    CanUserResizeRows .     GridLinesVisibility$
 None=� �� SelectedItem$ Flase��   AutoGenerateColumns .     CanUserResizeColumns .    CanUserReorderColumns .  $
��auto�����	#FFE6E6E6���	#FFF0F0F0$��	-5,7,3,55q�$��14(� � 	Resources =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� BasedOn �� 
TargetType �� Property �� Value��%�   '        7 �� 8   "  .���  � �� �	#DD008B8B��  � ��
 �������  � ��$
 Bold"���  � ��$ 28����  � ��$ Center=�    RowStyle  �� )  #System.Windows.Controls.DataGridRow"  ! �� Triggers! �� " �� Binding" �� *State+# �� Value
# Danger.a���  � ��
 �Gc��$   Columns$ 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn%   Header$%  ��&   Width5	  /System.Windows.Controls.DataGridLengthConverter$& SizeToCells	 '   
IsReadOnly' . (   CellTemplate( �� .R��� $��16��$��16������ *	List1Icon+0
  *System.Windows.Controls.DataGridTextColumn
  $	& 158	 $% Process��1  +System.Windows.Controls.DataGridBoundColumn)   Binding) �� *	Process+*   ElementStyle* �� " ��.���  �@�ToolTip �� *	Process+
  $& 71	 $% 
Start Time��) �� *	StartTime+* �� " ��.���  � ��$ Center=���  � ��$ Center=���  �@�ToolTip �� *	StartTime+
  $& 71	 $% End Time��) �� *	EndTime+* �� " ��.���  � ��$ Center=���  � ��$ Center=���  �@�ToolTip �� *	EndTime+
  $& 71	 $% Duration��) �� *
Duration+* �� " ��.���  � ��$ Center=���  � ��$ Center=���  �@�ToolTip �� *
Duration+  -   $ URLView��$��1��$��0��$��3�� .  .  $
 None=� .   .  .  $
��auto�����	#FFE6E6E6���	#FFF0F0F0$��0,7,3,55q�$��14(�+ %� IsVirtualizing+ .   �� *SelectedRow+, �� Mode$, TwoWay=�- � ContextMenu- �� -   . ��Name$
. menu��
���   �$��1q�.@�m� -   $��Copy URL��m� -   $��	Go to URL�� *  $System.Windows.Controls.DataGridCell/ P� Property0 P� Value1 ^� 
Conditions2 �� Binding3 �� RelativeSource4 �� Value5 �� AncestorType��%�  '        '  �    7 �� "  ! P� / �@ 
IsSelected0 . . ���  � ��# � ����  � ��# � ����  � ��# � ��^� 1 �� 2 �� *
IsSelected+3 �� *Self+4 True�� 2 �� *IsKeyboardFocusWithin+3 �� $5 DataGridL�	4 False.0���  � ��# � ����  � ��# � ����  � ��# � ��^� 1 �� 2 �� *
IsSelected+3 �� *Self+4 True�� 2 �� *ContextMenu.IsOpen+3 �� $5 DataGridL�4 True.0���  � ��# � ����  � ��# � ����  � ��# � ���� 8   "  .���  � �� �	#DD008B8B��  � ��
 �������  � ��$
 Bold"���  � ��$ 28����  � ��$ Center=�  �� "  ! �� " �� *State+
# Danger.a���  � ��
 �Gc��$   $%  ��$& SizeToCells	 ' . ( �� .R��� $��16��$��16������ *	List2Icon+
  $& 71	 $% 
Start Time��) �� *	StartTime+* �� " ��.���  � ��$ Center=���  � ��$ Center=���  �@�ToolTip �� *	StartTime+
  $& 71	 $% End Time��) �� *	EndTime+* �� " ��.���  � ��$ Center=���  � ��$ Center=���  �@�ToolTip �� *	EndTime+
  $& *	 $	% URL��) �� *strURL+* �� " ��.���  �@�ToolTip �� *strURL+  -   $ 	list2View��$��0,7,-5,0q�$��0��$��2�� .  .  $
 None=�$ Flase�� .   .   .  $
��auto�����	#FFF0F0F0���	#FFE6E6E6$��14(� ��%�   '        7 �� 8   "  .���  � �� �	#DD008B8B��  � ��
 �������  � ��$
 Bold"���  � ��$ 28����  � ��$ Center=�$   $%  ��$& SizeToCells	 ' . ( �� .R��� $��16��$��16������ *	List2Icon+
  $	& 165	 $% Process��) �� *	Process+* �� " ��.���  �@�ToolTip �� *	Process+
  $& 65	 $% Duration��) �� *Time+* �� " ��.���  � ��$ Center=���  � ��$ Center=���  �@�ToolTip �� *Time+� $��1��$��2��$��2�����	#FFF0F0F0$��
0,0,-5, 30q� �� $��28 ��� $��* � �� $	��100 ��� $��* �.+��� $��0��$��2��$��0�����	#DD008B8B$��28��.+��� $��Time Information��$��14(�$��Center=�
�������$��Center=�$��28��$
��Bold"�  LiveCharts.Wpf.PieChart  $��0��$��1��$��90��$	��0.2q�$��14(�$��0,0,0,0q�&   LiveCharts.Wpf.Charts.Base.Chart6   	Hoverable6 . 7   DataTooltip7 Z� 8   Series8   LiveCharts.Wpf.PieSeries  -   9  Name$9 WorkPie��  LiveCharts.Wpf.Series:   Title$
: Work��;   Values;  5LiveCharts.Wpf.Converters.NumericChartValuesConverter$; 0 <   
DataLabels< .  =   Fill= �#00B400  -   $9 RestPie��$
: Rest��$; 0 < .  = �#B40000  -   $9 FreePie��$
: Left��$; 10 < .  � $��1��$��1�� �� $��* ��� $��* ��� $��* � �� $��40 ��� $��* ��� $��10 �.+��� $��0��$��0��$��0,14,0,0q�$
��Work��$��13(�$
��Left=�$��Center=�$��Center=�$��28���� $��1��$��0��$��0,0,0,0q�$
��Rest��$��13(�$
��Left=�$��Center=�$��Center=�$��28���� $��2��$��0��$��0,0,0,14q�$
��Left��$��13(�$
��Left=�$��Center=�$��Center=�$��28���� $��0��$��1��$��0,15,0,0q�$��Stretch=�$��Center=����	#FF515151$��1q�._��� $��13��$ 
Horizontal=�$
��Left=�$��Center=�$	��115��.+��� -   > ��Name$> WorkRect�����#00B400$��60���� -    $> 
unWorkRect�����	#FFFFFFFF$	��132���� $��1��$��1��$��0,0,0,0q�$��Stretch=�$��Center=����	#FF515151$��1q�._��� $��13��$ 
Horizontal=�$
��Left=�$��Center=�$	��115��.+��� -!   $> RestRect�����#B40000$��24���� -"   $> 
unRestRect�����	#FFFFFFFF$	��132���� $��2��$��1��$��0,0,0,15q�$��Stretch=�$��Center=����	#FF515151$��1q�._��� $��13��$ 
Horizontal=�$
��Left=�$��Center=�$	��115��.+��� -#   $> LeftRect�����	#FF00A2FF$��60���� -$   $> 
unLeftRect�����	#FFFFFFFF$	��132���� $��0��$��1��$��	0,16,20,0q�$��Right=�.+��� -%   ? ��Name$? _WorkPie��$	��100��$
��0, 0q�$��15��$��SemiBold"�$
��Left=��� $��1��$��1��$��0,0,20,0q�$��Right=�.+��� -&   $? _RestPie��$	��100��$
��0, 9q�$��15��$��SemiBold"�$
��Left=��� $��2��$��1��$��	0,0,20,15q�$��Right=�.+��� -'   $? _LeftPie��$	��100��$
��0, 1q�$��15��$��SemiBold"�$
��Left=��� $
��Work��$��13(�$
��Left=�$��Center=�$��Center=�$��	0,25,0,84q�$��28��� $��2��$��0�����	#DD575757$��	-5,5,3,30q�.+��� -(   @ ��Name$@ processFilter��$
��Left=�$��23��$��1,3,0,3q�A �� TextWrapping$
A Wrap=�$��Filter by process��$��Center=�$	��200��B �� TextAlignment$
B Left=�$��Center=��� -)   C ��Name$C ALLProcessCheckBox��  RoundedCheckBoxStyle#���  $��13(����	#FFFFFFFF$��Forbidden processes��$��23��$��Right=�$��0,2,8,0q�� $��2��$��1�����	#DD575757$��0,5,3,30q�.+��� -*   $@ 	urlFilter��$
��Left=�$��23��$��1,1,0,1q�$
A Wrap=�$��Filter by URL��$��Center=�$	��304��$
B Left=�$��Center=��� -+   $C ALLURLCheckBox��#���  $��13(����	#FFFFFFFF$��Danger URLs��$��23��$��Right=�$��0,2,8,0q��� $��6��$
��10,1q����	#FF838383$��1q�._��� -,   $��
VoiceStack��$��3��$	��Top=�$ 
Horizontal=�$��0,0,0,0q�$��36���� --   $��6��.+��� $��	21,12,0,0q�$��SemiBold"�$
��Left=�Audio�� $��5��$
��10,1q����	#FF838383$��1q�._��� -.   $��ProcessStack��$��3��$ 
Horizontal=�$��0,0,0,0q��� -/   $��5��.+��� $��	21,12,0,0q�$��SemiBold"�$
��Left=�Applications�� $��4��$
��10,1q����	#FF838383$��1q�._��� -0   $��	TimeStack��$��1��$ 
Horizontal=�$��0,0,0,0q��� -1   $��4��.+��� $��	21,12,0,0q�$��SemiBold"�$
��Left=�Computer Usage�� $ 
Horizontal=�$��	12,0,12,0q�$��2��$��2������ D �� ImageSource$9D 3/Monitor.TaskView;component/Resource/Ruler copy.png��� $��2��$��7��.+��� .+��� -2   $	 TimeLine��$��25��$��19��$��-8,7,0,0q�$��/Resource/line.png��$��Uniform=�$��Bottom=��� -3   E ��Name$E grd_RedLine��F �� Left$F 0��
G �� Top$G 0�����	#FFe2830f$	��124��$��3��$��0,35,0,0q�� $��2��$��7�� �� $
��934* ��� $
��259* �.+��� $��2��.+��� -4   $E grd_GrayLine��$F 0��$G 0�����	#FF636363$	��142��$��1��$��0,14,0,0q�$y�Hidden=�� $��9��.+��� .+��� -5   $E 	grd_Popup��$
��Left=�$	��Top=�$F 0��$G 0��