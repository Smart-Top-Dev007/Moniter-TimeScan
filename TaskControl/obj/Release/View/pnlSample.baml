   M S B A M L   `   `   `  ���� N  JMonitor.TaskControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskControl.ViewMonitor.TaskControl.View  m iSystem.Windows.Forms.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�mclr-namespace:System.Windows.Forms.DataVisualization.Charting;assembly=System.Windows.Forms.DataVisualization/System.Windows.Forms.DataVisualization.Charting U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf (    "Monitor.TaskControl.View.pnlSample   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:Monitor.TaskControl.View     �winformchartmclr-namespace:System.Windows.Forms.DataVisualization.Charting;assembly=System.Windows.Forms.DataVisualization     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     -   
���   �$��1q�$	��500��$
��auto��.��� -   
�������  � ColumnDefinitions  ��  � RowDefinitions �� $��30 ��� $	��470 �.+�� -    �Name$ UserInfoHeader�����	#DD008B8B$��2��$��2�� J� Opacity$ 0.985Q�  �� $
��0.4* ��� $��1* ��� $
��1.2* ��� $
��1.5* ��� $
��1.4* ��� $
��0.6* ��� $
��0.7* ��� $
��0.7* ��� $
��0.7* ��� $
��0.7* ��� $
��1.8* ��� $
��0.6* ��� $
��0.7* � �� $��30 �.+�� $��0��.+���  �� Orientation$ 
Horizontal=�.+�A� -    A�Name$ eliForbiddenProcess��$
��25,0q�$��Center=�$��Center=�
�������$��13��$��13��� $��1��.+��� -    ��Name$ fldName��$
��Name��$��15(�$��Center=�
�������$��Center=�� $��2��.+��� -   $ 	fldIPAddr��$��
IP Address��$��14(�$��Center=�
�������$��Center=�$��28��� $��3��.+��� -   $ fldComputerName��$��Computer Name��$��14(�$��Center=�
�������$��Center=�$��28��� $��4��.+��� -   $ 	fldOSDate��$��OS Date��$��14(�$��Center=�
�������$��Center=�$��28��� $��5��.+��� -	   $ fldAudio��$��Audio��$
��Bold"�$��17(�$��Center=�
�������$��Center=�$��30��� $��6��.+��� -
   $ fldURL��$	��URL��$
��Bold"�$��17(�$��Center=�
�������$��Center=�$��30��� $��7��.+��� -   $ fldDownload��$��DownLoad��$��17(�$
��Bold"�$��Center=�
�������$��Center=�$��30��� $��8��.+��� -   $ 	dangerURL��$��
Danger URL��$��17(�$
��Bold"�$��Center=����#DC143C$��Center=�$��30��� $��9��.+��� -   $ fldForbiden��$��Forbiden��$��17(�$
��Bold"�$��Center=����#DC143C$��Center=�$��30��� $��10��  �� $
��0.4* ��� $
��1.5* �.+��� -    ��Name$ progressInspect��$��0��$��21��$��26��$��/Resource/0.png��$��Uniform=� J� RenderTransformOrigin$ 0.396,1.005&�$��Center=�$��Right=�$��0,3,0,0q��� -   $ inspect��$��1��$��0% (0/0)��   FontAwesome#��[  $��14(�$
��Left=�
�������$��Center=�$��30��$��0,9,0,0q�� $��11��.+��� -   $ workTime��$��	Work Time��$��17(�$
��Bold"�$��Center=�
�������$��Center=�$��30��� $��12��$��Center=�.+��� $��20��$��20��$��/Resource/message.png��$��Uniform=�$ 0.396,1.005&�$��Bottom=�$��	20,0,29,4q��� -   	 ��Name$	 message_body��$��	25,2,0,10q�
���  ��$��Center=�
 �� CornerRadius$
 50��$��16��$��16��._��� -   $ message_count��$��16��$��Center=�$��0��$��	0,0.0,0,0q�
�������$��0,0,0,-3q�� $��1��
������� �� $
��Auto �$��0���� $	��115 ��� $	��234 �  �� $	��9.5 ��� $
��96.5 ��� $��* ��� $��30 �.+�� $��2��$��2��$��0��$��2��.+��� -   $ AVARTA��$��80��$��80��$��12,0,14,140q� K� ZIndex$ 1��$��/Resource/Connect_Off.png��$��Uniform=�$ 0.396,1.005&�$��Bottom=��� $ 
Horizontal=�$
��Left=�$��Center=�$��35,10,0,-40q�.+��� $
��Wrap=�$��9(�$��Audio < 5 mins��$
��Left=� �� TextAlignment$
 Left=�$��Center=�$��0,0,0,0q�$��93���� $
��Left=�$��Center=����	#FF515151$��1q�$��15,10,0,-40q�._��� $��13��$ 
Horizontal=�$
��Left=�$��Center=�$��13��.+��� ���	#FF1656E0$��13���� $ 
Horizontal=�$
��Left=�$��Center=�$��35,70,0,-40q�.+��� $
��Wrap=�$��9(�$��Audio > 5 mins��$
��Left=�$
 Left=�$��Center=�$��0,0,0,0q�$��93���� $
��Left=�$��Center=����	#FF515151$��1q�$��15,70,0,-40q�._��� $��13��$ 
Horizontal=�$
��Left=�$��Center=�$��13��.+��� ���#1b9dc3$��13���� $ 
Horizontal=�$��Center=�$��Center=�$��
10,205,0,0q�.+��� $
��Wrap=�$
��Work��$
 Left=�$��Center=�$��0q��� $
��Left=�$��Center=����	#FF515151$��1q�$��15,200,0,-40q�._��� $��13��$ 
Horizontal=�$
��Left=�$��Center=�$��80��.+��� -    ��Name$ WorkRect�����#00B400$��60���� -   $ 
unWorkRect�����	#FFFFFFFF$��20���� $��8,229,0,-20q�$��Center=�.+��� -    ��Name$ workText��$��99��$
��0, 0q�$��15��$��SemiBold"�$
��Left=��� $ 
Horizontal=�$��Center=�$��Center=�$��10,242,0,-40q�.+��� $
��Wrap=�$
��Rest��$ Center=�$��Center=�$��0q��� $
��Left=�$��Center=����	#FF515151$��1q�$��15,260,0,-55q�._��� $��13��$ 
Horizontal=�$
��Left=�$��Center=�$��80��.+��� -   $ RestRect�����#B40000$��24���� -   $ 
unRestRect�����	#FFFFFFFF$��56���� $��0��$��1��$��8,267,0,-50q�$��Center=�.+��� -   $ restText��$��99��$
��0, 0q�$��15��$��SemiBold"�$
��Left=��� $ 
Horizontal=�$��Center=�$��Center=�$��10,283,0,-75q�.+��� $
��Wrap=�$��
Disconnect��$
 Left=�$��Center=�$��0q��� $
��Left=�$��Center=����	#FF515151$��1q�$��15,290,0,-100q�._��� $��13��$ 
Horizontal=�$
��Left=�$��Center=�$��81��.+��� -   $ DisconnectRect�����#FFB700$��60���� -   $ unDisconnectRect�����	#FFFFFFFF$��20���� $��8,304,0,-85q�$��Center=�.+��� -   $ disconnectText��$��99��$
��0, 0q�$��15��$��SemiBold"�$
��Left=�� $��2��$��2��$��	0,0,0,-37q� �� �� �� �� �� $��30 �  �� $	��250 ��� $
��423* ��� $��0* ��� $	��280 �.+�&   System.Windows.Controls.DataGrid  -     Name$ 	list2View��$��	0,7,0,-53q�$��0��$��0��$��5��   CanUserResizeRows .     
IsReadOnly .    GridLinesVisibility$
 None=� �� SelectedItem$ Flase��   AutoGenerateColumns .     CanUserResizeColumns .    CanUserReorderColumns .  
����������	#FF676868$��12(� � 	Resources =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� BasedOn �� 
TargetType �� Property �� Value��%�   '        7 �� 8   "  .���  � �� �	#FFFFA200��  � ��
 �������  � ��$ Normal"���  � ��$ 22����  � ��$ Center=�   RowStyle �� )  #System.Windows.Controls.DataGridRow"   �� Triggers ��  �� Binding �� *State+ �� Value
 Danger.a���  � ��
 �Gc��    Columns  4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn!   Header$!  ��"   Width5  /System.Windows.Controls.DataGridLengthConverter$" SizeToCells #   
IsReadOnly# . $   CellTemplate$ �� .R��� $��16��$��16������ *	List2Icon+0  *System.Windows.Controls.DataGridTextColumn  $	" 153 $! Process��1  +System.Windows.Controls.DataGridBoundColumn%   Binding% �� *	Process+&   	CellStyle& �� *	  $System.Windows.Controls.DataGridCell" 	 .���  �@�ToolTip �� *	Process+  $" * $! Duration��% �� *Time+& �� " 	 .���  �@�ToolTip �� *Time+� $��4��$��0�����	#DD575757$��
0,83,0,-75q�.+��� -   ' ��Name$' processFilter��$
��Left=�$��24��$��1,3,0,3q�( �� TextWrapping$
( Wrap=�$��Filter by process��$��Center=�$��95��) �� TextAlignment$
) Left=�$��Center=��� -   * ��Name$* ALLProcessCheckBox��  RoundedCheckBoxStyle#���  $��12(����	#FFFFFFFF$��Forbidden processes��$��Right=�$��	0,-1,3,-0q�$	��140��  -    $ URLView��$��	5,7,0,-53q�$��0��$��1��$��5�� .   . $
 None=� .   .  .  $
��auto��
����������	#FF676868$��12(� �� *SelectedRow++ �� Mode$+ TwoWay=�, � ContextMenu, �� -!   - ��Name$
- menu��
���   �$��1q�.@�m� -"   $��Copy URL��m� -#   $��	Go to URL�� . P� Property/ P� Value0 ^� 
Conditions1 �� Binding2 �� RelativeSource3 �� Value4 �� AncestorType��%�  '	        '  �    7 �� " 	  P� . �@	 
IsSelected/ . . ���  � ��# � ����  � ��# � ����  � ��# � ��^� 0 �� 1 �� *
IsSelected+2 �� *Self+3 True�� 1 �� *IsKeyboardFocusWithin+2 �� $4 DataGridL�	3 False.0���  � ��# � ����  � ��# � ����  � ��# � ��^� 0 �� 1 �� *
IsSelected+2 �� *Self+3 True�� 1 �� *ContextMenu.IsOpen+2 �� $4 DataGridL�3 True.0���  � ��# � ����  � ��# � ����  � ��# � ���� 8   "  .���  � �� �	#FFFFA200��  � ��
 �������  � ��$ Normal"���  � ��$ 22����  � ��$ Center=� �� "   ��  �� *State+
 Danger.a���  � ��
 �Gc��    $!  ��$" SizeToCells # . $ �� .R��� $��16��$��16������ *	List2Icon+  $" 75 $! 
Start Time��% �� *	StartTime+5   ElementStyle5 �� " ��.���  �@�ToolTip �� *	StartTime+  $" 75 $! End Time��% �� *	EndTime+5 �� " ��.���  �@�ToolTip �� *	EndTime+  $" * $	! URL��% �� *strURL+5 �� " ��.���  �@�ToolTip �� *strURL+� $��4��$��1�����	#DD575757$��
5,83,0,-75q�.+��� -$   $' 	urlFilter��$
��Left=�$��24��$��1,3,0,3q�$
( Wrap=�$��Filter by URL��$��Center=�$	��267��$
) Left=�$��Center=��� -%   $* ALLURLCheckBox��#���  $��12(����	#FFFFFFFF$��Danger URLs��$��23��$��Right=�$��0,-1,8,0q��� $��	5,7,0,-75q�$��5��$��3�����	#FF515151$��1q�._�� $��0,0,0,-1q� �� $��22 ��� $��42 ��� $��42 ��� $��11 ��� $��42 ��� $��42 ��� $��11 ��� $��42 ��� $��42 ��� $	��50* �  �� $��80 ��� $	��100 ��� $��1* �.+��� $��0��$��1�����	#FFFFA200$��Stretch=��� $��0��$��1��$��2�����	#FFCFCFCF]� $��1��$��2��$��0��$��Center=�$��0,0,0,0q�.+�
  LiveCharts.Wpf.PieChart
  $
��Left=�$	��0.2q�$��58��$��20(�&   LiveCharts.Wpf.Charts.Base.Chart6   	Hoverable6 . 7   DataTooltip7 Z� 8   Series8   LiveCharts.Wpf.PieSeries  -&   9  Name$9 WorkPie��  LiveCharts.Wpf.Series:   Title$
: Work��;   Values;  5LiveCharts.Wpf.Converters.NumericChartValuesConverter$; 23 <   
DataLabels< .  =   Fill= �#00B400  -'   $9 RestPie��$
: Rest��$; 4 < .  = �#B40000  -(   $9 DisconnectPie��$: 
Disconnect��$; 6 < .  = �#FFB700  -)   $9 FreePie��$
: Free��$; 10 < .  ]� $��1��$��2��$��1��$��Center=�$��0,0,0,0q�.+��� $��1��$	��Top=�$
��Wrap=�$��Work Time :��$��3��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��65��$��0,15,0,0q��� $��3��$	��Top=�$��0,3,0,0q�$
��Wrap=�$��Rest Time : ��$��3��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��59���� $��3��$	��Top=�$��0,0,0,7q�$
��Wrap=�$��Disconnect Time : ��$��3��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$	��100���� -*   $ 	TotalTime��$��1��$��2��$
��Wrap=�$�� ��$��2��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��93��$��0,0,0,36q��� -+   $
 Rest��$��1��$��2��$
��Wrap=�$�� ��$��2��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��93��$��0,5,0,0q��� -,   $ 
Disconnect��$��1��$��2��$
��Wrap=�$�� ��$��2��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��93��$��0,45,0,0q��� $��3��$��0��$��1�����	#FFFFA200�� $��3��$��1��$��2�����	#FFCFCFCF]� $��4��$��2��$��0��$��Center=�$��0,0,0,0q�.+��� $
��Left=�$��45��$��45��$��/Resource/RestTime.png��$��Uniform=�$��
0, 0, 0, 0q�> J� OpacityMask> �� ? �� Color$? 	#FFE2E2E2��@ �� Opacity$	@ 0.7Q�]� $��4��$��2��$��1��$��Center=�$��0,0,0,0q�.+��� $	��Top=�$��0,15,0,0q�$
��Wrap=�$��Session Time :��$��3��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��98���� $	��Top=�$
��Wrap=�$��Capture Time : ��$��3��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��0,3,0,0q�$��98���� $	��Top=�$
��Wrap=�$��Active Time : ��$��3��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��0,0,0,7q�$	��101���� --   $ SessionTime��$��4��$��2��$
��Wrap=�$�� ��$��2��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��93��$��0,0,0,36q��� -.   $ CaptureTime��$��4��$��2��$
��Wrap=�$�� ��$��2��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��93��$��0,5,0,0q��� -/   $ 
ActiveTime��$��4��$��2��$
��Wrap=�$�� ��$��2��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��93��$��0,45,0,0q��� $��6��$��1�����	#FFFFA200$��Stretch=��� $��6��$��1��$��2�����	#FFCFCFCF]� $��7��$��2��$��0��$��Center=�$��0,0,0,0q�.+��� $
��Left=�$��40��$��40��$��/Resource/SlideImage.png��$��Uniform=�$��
0, 0, 0, 0q�> �� $? White��$	@ 0.7Q�]� $��7��$��2��$��1��$��Center=�$��0,0,0,0q�.+��� $	��Top=�$��0,15,0,0q�$
��Wrap=�$��Slide Image : ��$��3��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��98���� $	��Top=�$
��Wrap=�$��ScreenShot : ��$��3��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$	��100��$��0,0,0,7q��� -0   $ Slide��$��7��$��2��$
��Wrap=�$�� ��$��2��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��93��$��0,0,0,36q��� -1   $ Capture��$��7��$��2��$
��Wrap=�$�� ��$��2��$
 Left=�$
��Left=�$��Center=�$��18��$��12(�$��93��$��0,22,0,0q��� $ 
Horizontal=�$��0��$��3��$��Center=�$��Center=�$��21,2,18,-10q�$��38��$	��232��$��9��.+��� -2   A ��Name$A btnShowState��  RoundedSettingsButtonStyle1#���  
�������$	��0 0q����	#FFE8850E$��0q�$��73��$��Remote��$��0,0,0,10q���Z� �� -3   $A SlideButton��  RoundedSettingsButtonStyle2#���  ���	#EE228B22
�������$��0q�$��74��$
��Play��$��5,0,0,10q���Z� �� -4   $A ExplorerButton��  RoundedSettingsButtonStyle#���  ���	#FF4682B4
�������$��0q�$��74��$��Explorer��$��5,0,0,10q���Z� � $��1��$��0,30,0,0q�$��2��.+��� $ 
Horizontal=�$
��Left=�$��Center=�$��
10,-50,0,0q�.+��� B � ToolTip$B State��$	����$��Center=�$2��,pack://application:,,,/Resource/#FontAwesome)����	#FF555555$��15(��� $
��Wrap=�$��	 WorkTime��$ Center=�$��Center=��� $ 
Horizontal=�$
��Left=�$��Center=�$��10,0,0,0q�.+��� $B State��$	����$��Center=�$2��,pack://application:,,,/Resource/#FontAwesome)����	#FF555555$��15(��� $
��Wrap=�$�� Process��$ Center=�$��Center=��� $ 
Horizontal=�$
��Left=�$��Center=�$��	10,50,0,0q�.+��� $B State��$	����$��Center=�$2��,pack://application:,,,/Resource/#FontAwesome)����	#FF555555$��15(��� $
��Wrap=�$�� Audio��$ Center=�$��Center=�� $��1��$��2��$��0,20,0,0q�  �� �� $��0* �.+��� $��
0,-16,0,82q����� C �� ImageSource$<C 6/Monitor.TaskControl;component/Resource/Ruler copy.png��$	@ 0.9Q��� ���	#FF515151$��1q�$��	0,16,0,54q�._��� -5   D ��Name$D pnlProgTime��$ 
Horizontal=�$��0,0,0,0q�$
��auto���� -6   $	 borderProgress�����	#FF515151$��1q�$��	0,44,0,27q�._��� -7   $D pnlProgProcess��$ 
Horizontal=�$��24��$��0,0,0,0q�$
��auto���� ���	#FF515151$��1q�$��0,71,0,1q�._��� -8   $D pnlProgAudio��$ 
Horizontal=�$��0,0,0,0q�$��20��$
��auto��� $��0��$��2��  �� $
��339* ��� $��4* ��� $	��85* �� $��10��$��10��.+��� .+��� -9   $D 	grd_Popup��$
��Left=�$	��Top=�E �� Left$E 0��
F �� Top$F 0��