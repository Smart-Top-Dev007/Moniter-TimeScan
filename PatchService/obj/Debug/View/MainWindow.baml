   M S B A M L   `   `   `  ����G  CPatchService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PatchService.ViewPatchService.View  5   	   C&clr-namespace:PatchService.UserControlPatchService.UserControl  5   	   =#clr-namespace:PatchService.ResourcePatchService.Resource  5   	   "    PatchService.View.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PatchService.View     5   	   ;controls&clr-namespace:PatchService.UserControl     5   	   8resource#clr-namespace:PatchService.Resource     5   	   -   5        $	��720��5	      $
��1280��6'     � Icon$'  !pack://application:,,,/net-on.ico��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ 	CanResize=�6.   $��1q�5   	   
���   �$
��1280��6H   $	��720��6X    � Cursor$ Arrow��6�    � Title$ Patch Server��5       � 	Resources 5         
SlideRight �� TargetProperty t� From	 t� To	 g� AccelerationRatio
 g� Duration  	SlideLeft,   &PatchService.View.NameToImageConverter  NameToImageConverter��%9  &	        &	 �     &	     �� 5   
   .��5      t� $ Margin�6!   $ 0,0,0,0X�6D   $ 
-230,0,0,0X�6S   $	 .9Q�6c   $
 0:0:0.3D�6z   5      �� 5   
   .��5      t� $ Margin�6!   $ 
-230,0,0,0X�6D   $ 0,0,0,0X�6V   $	 .9Q�6c   $
 0:0:0.3D�6z   5        5   
   6%   5      .��5      � $��0,0,0,0q�6   ���	#FF232323 � ColumnDefinitions 5&   
   �� 5(      $	��65* �6   �� 5)      $	��62* �6   �� 5*      $��2 �6    � RowDefinitions 5,   
   �� 5-      $��50 �6   �� 5.      �� 5/      $��4 �6   .+�54   
   � -   5        $��0��54      $��0��6   $��3��6,   $��	0, 0,-2,0q�6@   ���	#FF40A6D1.+�58      /   )PatchService.UserControl.HeaderDecoration  �� 59      -   5         ��Name$ AVARTA��59      $��25��6$   $��25��60    K� ZIndex$ 1��6;   $
��Left=�6L   $��	14,0,0,10q�6g   $��/Resource/net-on.png��6z   $��Uniform=�6�    J� RenderTransformOrigin$ 0.396,1.005&�6�   $��Bottom=�6�   �� 5;      -   5         ��Name$ title��5;      $�� ��6#   $
��Left=�5<      $��Center=�6/   $��25(�6J   $��SemiBold"�6X   
�������$��43,4,0,4q�5=   '    J� Effect 5>      3  -System.Windows.Media.Effects.DropShadowEffect  5?         Opacity$	 0.2Q�6'   5A      �� 5B      $��Center=�6    �� Orientation$ 
Horizontal=�64   $��350,1,517,9q�5C      $��40��6.   5E      �� 5F      $��2��6   $��2��6-   $��Bottom=�6=   $ 
Horizontal=�6X   $��0,0,5,0q�5G      $��Right=�6*   .+�5H      �� -   5        $	����5H        CloseMinMaxButtonStyle#���  6%   $��40��6V   $��40��6a    � ToolTip$
 Hide��6m   
������ 
������ 
�������$2��,pack://application:,,,/Resource/#FontAwesome)�5J      $��20(�6S   $��Bottom=�5K      |�.  6P   �� 5P      -   5         ��Name$ 	maxButton��5P      $��40��6,   $��40��67   $ Resize��6C   |�.  $	����6�   #���  6�   
������ 
������ 
�������$2��,pack://application:,,,/Resource/#FontAwesome)�5R      $��20(�6S   $��Bottom=�5S      �� 5V      -   5        $��40��5V      $��40��6$   |�.  $	����6^   #���  6j   
������ 
������ 
�������$
 Exit��5W   _   $2��,pack://application:,,,/Resource/#FontAwesome)�5X      $��20(�6S   $��Bottom=�5Y      5Z      5[      �� 5]   
   $��1��6    �� TabStripPlacement$
 Left=�6"   $��0,10,0,0q�6;   $��3��6M   $��0��6a      LeftTabColumnTabControlStyle#���  5^      
���������5_      Z� .@�5`      ��   LeftTabColumnTabItemStyle#���  6   ��6K   Z� ��5a      �� 5b      $ State of Installing��6   $	����6:   $��Center=�6M   $2��,pack://application:,,,/Resource/#FontAwesome)�5c       
�������$��Center=�5d       $��35(�6;   $��	-4,0,-3,0q�6J   .��5f      � ���#303030.+�5g      �� $��	0,-10,0,0q�6!   
������� 5h       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers P� Property P� Value  �� 
TargetName��%J  '��       �� 5i      " ��6$   .�5j   "   ��  � �� 5k   &   �� 5l   *   " ��6:   .R�5m   .   � $��Panel��63   .+�5n   2   �� $��Border��69   $��1,1,1,1q�6H   
�������$ 2��6{   �� 5o   2   $ ContentSite��6C   $��Center=�6Y   $��Center=�6u   $��Header��6�   $
��10,2q�6�   5p   /    5q   .   P� 5r   2    �@��
IsSelected . . �5s   6   �� $  Panel��6=    � �� �#e976006f   5t   3   P� 5u   2    �@��
IsSelected .  . �5v   6   �� $  Panel��6=    � �� �#3030306f   5w   3   5y   +   5{   #   5|      5}      .@�5~      �� $��40��6"   $��-1,9,0,0q�6.   ���#e97600��5      �� 5�   "   $ 
Horizontal=�6-   $	��150��6F   .+�5�   &   �� $��Installed State��6,   $��	0,-10,0,0q�6F   $��Center=�6Y   $��Center=�6}   
�������$��16(�6�   $	��150��6�   $��31��6�   5�   #   .��5�      �  5�   "   �� 5�   &   $��70 �64   �� 5�   &   $	��92* �64    5�   "   �� 5�   &   $��* �67   .+�5�   "   �  5�   &   �� 5�   *   $	��25* �6;   �� 5�   *   $	��10* �6;   �� 5�   *   $	��15* �6;   �� 5�   *   $	��15* �6;   �� 5�   *   $	��14* �6;   �� 5�   *   $	��14* �6;   .+�5�   &   �� $
��Left=�61   $��Bottom=�6L   $ 
Horizontal=�6g   .+�5�   *   �� -   5        $ nTotal��5�   0   $��All Users : ��6@   $��13(�6W   $
��Bold"�6e   $��
10,-5,0,-5q�6w   �� 5�   *   -	   5        $ nServer��5�   0   $��
Servers : ��6A   $��13(�6V   $
��Bold"�6d   $��
10,-5,0,-5q�6v   �� 5�   *   -
   5        $ nClients��5�   0   $��
Clients : ��6B   $��13(�6W   $
��Bold"�6e   $��
10,-5,0,-5q�6w   5�   '   �� 5�   &   $��1��61   $ 
Horizontal=�6A   $��Bottom=�6Z   $
��Left=�6u   .+�5�   *   �� $ 
Horizontal=�65   .+�5�   .   A� $��13��66   $��13��6A   ���#2DAAE16M   �� 5�   .   $��Server��64   $��13(�6E   $��	0,-5,0,-5q�6S   5�   +   �� 5�   *   $ 
Horizontal=�65   .+�5�   .   A� $��13��66   $��13��6A   ���#FEA5006M   �� 5�   .   $��Client��64   $��13(�6E   $��	0,-5,0,-5q�6S   5�   +   5�   '   �� 5�   &   $��2��61   $��Center=�6A   $��Bottom=�6^   $ 
Horizontal=�6y   .+�5�   *   �� $��
User Role:��60   $��Right=�6E   $��Bottom=�6a   ���	#FF1D0909$��14(�6�   $��0,3,0,0q�6�   �� 5�   *   -   5        ! ��Name$! filter��5�   3   " J� 	AllowDrop" .  $��0,0,0,0q�6v   $	��100��6�   $��Center=�6�   $��Center=�5�   5   $ Filter��6W   $��1��6h   $��14(�6u   # �� 
IsReadOnly# . $ �� SelectedIndex$$ 0��6�   .@�5�   .   �� $	��All��6;   �� 5�   .   $��Server��6;   �� 5�   .   $��Client��6;   5�   +   5�   '   �� 5�   &   $��3��61   $��Bottom=�6A   $��0q�6\   $ Vertical=�6g   $��Center=�6~   .+�5�   *   �� $ 
Horizontal=�65   .+�5�   .   �� $��13��68   $��13��6C   ���#4bff006O   �� 5�   .   $��Bootable OS Patition��64   $��13(�6S   $��	0,-5,0,-5q�6a   5�   +   �� 5�   *   $ 
Horizontal=�65   .+�5�   .   �� $��13��68   $��13��6C   ���#ef00ff6O   �� 5�   .   $��Primary Patition��64   $��13(�6O   $��	0,-5,0,-5q�6]   5�   +   �� 5�   *   $ 
Horizontal=�65   .+�5�   .   �� $��13��68   $��13��6C   ���#ff51516O   �� 5�   .   $��Hidden Patition��64   $��13(�6N   $��	0,-5,0,-5q�6\   5�   +   5�   '   � 5�   &   $��4��6+   $
��Left=�6;   $��Bottom=�6V   .+�5�   *   �� -   5        $ 
nUninstall��5�   0   $��Uninstalled User: ��6D   $��13(�6a   $
��Bold"�6o   $	��0,0q�6�   5�   '   �� 5�   &   $��5��61   $��Center=�6A   $��Bottom=�6^   $ Vertical=�6y   .+�5�   *   �� .+�5�   .   �� -   5        $��	Export IP��5�   5   $	��120��6I   $��Right=�6U   $��0,0,0,4q�6q   ���#40A6D16�   5�   +   �� 5�   *   $ 
Horizontal=�65   .+�5�   .   �� $��Floor :��64   $��Right=�6F   $��Bottom=�6b   ���	#FF1D0909$��14(�6�   $��0,3,0,0q�6�   �� 5�   .   -   5        $! floor��5�   7   " .  $��0,0,0,0q�6x   $	��120��6�   $��Center=�6�   $��Center=�5�   5   $ Filter��6W   $��1��6h   $��14(�6u   # . $$ 0��6�   .@�5�   2   �� $	��All��6?   �� 5�   2   $��
1th7 floor��6?   �� 5�   2   $��
1th8 floor��6?   �� 5�   2   $��
1th9 floor��6?   �� 5�   2   $��
2th3 floor��6?   �� 5�   2   $��
2th4 floor��6?   �� 5�   2   $��
2th5 floor��6?   �� 5�   2   $��
3th1 floor��6?   �� 5�   2   $��
3th2 floor��6?   �� 5�   2   $��
3th3 floor��6?   �� 5�   2   $��
3th4 floor��6?   �� 5�   2   $��
3th5 floor��6?   5�   /   5�   +   5�   '   5�   #   � 5�   "   $��1��6'    5�   &   �� 5�   *   $��7* �6;   �� 5�   *   $��3* �6;   .+�5�   &   � $��0��6+   $��5,5,5,5q�6;   .+�5�   *   &   System.Windows.Controls.DataGrid  -   5        %  Name$% StateDataGrid��5�   3   &   SelectionUnit$& FullRow=�6J   '   SelectionMode$' Single=�6b   (   GridLinesVisibility$
( None=�6z   )   
IsReadOnly) . * �� SelectedItem$* Flase��6�   +   AlternatingRowBackground+ �	#FFe7f4fc,   AutoGenerateColumns, . -   CanUserResizeColumns- . .   CanUserReorderColumns. . ���	#FF676868$��0,0,0,0q�6N  $��14(�6_  /   	CellStyle/ 5�   .   �� 5�   2   *  $System.Windows.Controls.DataGridCell"  68   .�5�   6   ��  � ��$ 1,0,0,1q�6X   �� 5�   6    � ��
 ��ح�6T   5�   3    5�   .   =  7System.Windows.Controls.Primitives.DataGridColumnHeader0 �� BasedOn��%�   '        7 �� 5�   2   80   "  6q   .�5�   6   ��  � �� �	#CC4682B46S   �� 5�   6    � ��
 �����6S   �� 5�   6    � ��$
 Bold"�6S   �� 5�   6    � ��$ 32��6O   �� 5�   6    � ��$ Center=�6c   5�   3   5�   /   1   Columns1 5�   .   4  .System.Windows.Controls.DataGridTemplateColumn  5�   2   ,  &System.Windows.Controls.DataGridColumn2   Header$2 	User Role��6I   3   Width5	  /System.Windows.Controls.DataGridLengthConverter$	3 40*	 6\   4   
IsReadOnly4 . 5   CellTemplate5 5�   6   �� 5�   :   .R�5�   >   A� $��16��6F   $��16��6Q   ��6]   �� *Color+5�   ;   5�   3   0
  *System.Windows.Controls.DataGridTextColumn
  5�   2   $2 
IP Address��6E   $	3 80*	 6Y   1  +System.Windows.Controls.DataGridBoundColumn6   Binding6 6e   �� *Ip+7   ElementStyle7 5�   6   �� 5�   :   " ��6@   .�5   >   ��  � ��$ Center=�6b   �� 5  >    � ��$ 25��6W   �� 5  >    � ��$ Center=�6d   5  ;   5  3   
  5  2   $2 ComputerName��6E   $
3 100*	 6[   6 6h   �� *ComputerName+7 5  6   �� 5	  :   " ��6@   .�5
  >   ��  � ��$ Center=�6b   �� 5  >    � ��$ 25��6W   �� 5  >    � ��$ Center=�6d   5  ;   5  3   
  5  2   $2 
OS Created��6E   $
3 100*	 6Y   6 6f   �� *
SysCreated+7 5  6   �� 5  :   " ��6@   .�5  >   ��  � ��$ Center=�6b   �� 5  >    � ��$ 25��6W   �� 5  >    � ��$ Center=�6d   5  ;   5  3   
  5  2   $	3 30*	 6E   6 6Q   �� *BootOs+8   HeaderTemplate8 5  6   �� 5  :   .R�5  >   �� $��15��6I   $��Center=�6U   $��Center=�6p   .+�5  B   �� $��15��6L   $��15��6W   ���#4bff00$��Center=�6r   5   ?   5!  ;   7 5#  6   �� 5$  :   " ��6@   .�5%  >   ��  � ��$ Center=�6b   �� 5&  >    � ��$ 25��6W   �� 5'  >    � ��$ Center=�6d   5(  ;   5*  3   
  5+  2   $2 
Partitions��6E   $	3 30*	 6Y   6 6e   �� *	Partition+8 5,  6   �� 5-  :   .R�5.  >   �� $��15��6I   $��Center=�6U   $��Center=�6p   .+�5/  B   �� $��15��6L   $��15��6W   ���#ef00ff$��Center=�6r   50  ?   51  ;   7 53  6   �� 54  :   " ��6@   .�55  >   ��  � ��$ Center=�6b   �� 56  >    � ��$ 25��6W   �� 57  >    � ��$ Center=�6d   58  ;   5:  3   
  5;  2   $2 Hidden��6E   $	3 30*	 6U   6 6a   �� *Hidden+8 5<  6   �� 5=  :   .R�5>  >   �� $��15��6I   $��Center=�6U   $��Center=�6p   .+�5?  B   �� $��15��6L   $��15��6W   ���#ff5151$��Center=�6r   5@  ?   5A  ;   7 5C  6   �� 5D  :   " ��6@   .�5E  >   ��  � ��$ Center=�6b   �� 5F  >    � ��$ 25��6W   �� 5G  >    � ��$ Center=�6d   5H  ;   5J  3   5L  +   5M  '   � 5N  &   $��1��6+   $��5,5,5,5q�6;   .+�5O  *     -   5        $% 	UnClients��5O  3   $& FullRow=�6F   $' Single=�6^   $
( None=�6v   ) . $* Flase��6�   + �	#FFe7f4fc, . - . . . ���	#FF676868$��0,0,0,0q�6J  $��14(�6[  / 5P  .   �� 5Q  2   "  68   .�5R  6   ��  � ��$ 1,0,0,1q�6X   �� 5S  6    � ��
 ��ح�6T   5T  3    5V  .   ��%�   '        7 �� 5Z  2   80   "  6q   .�5[  6   ��  � �� �	#CC4682B46S   �� 5\  6    � ��
 �����6S   �� 5]  6    � ��$
 Bold"�6S   �� 5^  6    � ��$ 32��6O   �� 5_  6    � ��$ Center=�6c   5a  3   5b  /   1 5c  .   
  5d  2   $2 
IP Address��6E   $	3 80*	 6Y   6 6f   �� *Ip+7 5e  6   �� 5f  :   " ��6@   .�5g  >   ��  � ��$ Center=�6b   �� 5h  >    � ��$ 25��6W   �� 5i  >    � ��$ Center=�6d   5j  ;   5l  3   5n  +   5o  '   5p  #   � 5q  "   $��2��6(    5r  &   �� 5s  *   $��1* �6;   �� 5t  *   $��1* �6;   5  #   5�     5�     5�     5�     5�     �� 5�     #���  6   ��6K   Z� ��5�     �� 5�     -   5        $ Inspect��5�     $	����6.   $��Center=�6A   $2��,pack://application:,,,/Resource/#FontAwesome)�5�      
�������$��Center=�5�      $��35(�6;   $��	-4,0,-3,0q�6J   6]   .��5�     � ���#303030.+�5�     �� $��	0,-10,0,0q�6!   
������� 5�     ��%J  '��       �� 5�     " ��6$   .�5�  "   ��  � �� 5�  &   �� 5�  *   " ��6:   .R�5�  .   � $��Panel��63   .+�5�  2   �� $��Border��69   $��1,1,1,1q�6H   
�������$ 2��6{   �� 5�  2   $ ContentSite��6C   $��Center=�6Y   $��Center=�6u   $��Header��6�   $
��10,2q�6�   5�  /    5�  .   P� 5�  2    �@��
IsSelected . . �5�  6   �� $  Panel��6=    � �� �#e976006f   5�  3   P� 5�  2    �@��
IsSelected .  . �5�  6   �� $  Panel��6=    � �� �#3030306f   5�  3   5�  +   5�  #   5�     5�     .@�5�     �� $��40��6"   $��-1,9,0,0q�6.   ���#e97600��5�     �� 5�  "   $ 
Horizontal=�6-   $	��200��6F   $��37��6R   .+�5�  &   �� $��Inspect��6,   $��-10,-10,-10,0q�6>   $��Center=�6U   $��Center=�6y   $��16(�6�   $	��221��6�   
�������6�   5�  #   .��5�     � $��5,15,5,5q�6#   $��0��65   $��2��6B    5�  "   �� 5�  &   $��30 �64   �� 5�  &   $��* �64   .+�5�  "   � $��0��6'    5�  &   �� 5�  *   $	��61* �6;   �� 5�  *   $��1104* �6;   .+�5�  &   �� -   5        $ strCntServer��5�  ,   $	��150��6B   $�� ��6N   $��54,-4,892,4q�6Y   $��Center=�6n   $��14(�6�   $
��Bold"�6�   $��1��6�   (  "System.Windows.Controls.DatePicker  5�  &   -   5        9  Name$9 date_Picker��5�  1   $	��300��6F   $��17(�6R   $��Center=�6`   :   SelectedDateFormat$
: Long=�6}   |�.  $��371,-4,433,4q�6�   $��1��6�   �� 5�  &   -   5        $ imgBack��5�  ,   $��/Resource/Back.png��6=   $��687,-4,386,5q�6�   $ 1.302,1.035&�6�   $��1��6�   �� 5�  &   -   5        $ 
imgForward��5�  ,   $��/Resource/Forward.png��6@   $��733,-4,332,5q�6�   $ 12.351,-0.392&�6�   $��1��6�   �� 5�  &   -   5        $ btnToday��5�  -     RoundedSettingsButtonStyle1#���  6?   
�������$	��0 0q�5�  ,   ���	#FFE8850E$��56��6Q   $��0q�5�  0   $��Today��6D   $��804,-4,242,4q�6T   $ 0.341,0.217&�6�   $��1��6�   ��6   Z� 6�   5�  #   � 5�  "   $��1��6'   $��5,5,5,5q�64    5�  &   �� 5�  *   $��4* �6;   �� 5�  *   $��3* �6;   .+�5�  &     -   5        $% InspectDataGrid��5�  /   $& FullRow=�6H   $' Single=�6`   $
( None=�6x   ) . $* Flase��6�   + �	#FFe7f4fc, . - . . . ���	#FF676868$��0,0,0,0q�6L  $��14(�6]  $��2��6k  / 5�  *   �� 5�  .   "  64   .�5�  2   ��  � ��$ 1,0,0,1q�6T   �� 5�  2    � ��
 ��ح�6P   5�  /    5�  *   ��%�   '        7 �� 5�  .   80   "  6m   .�5�  2   ��  � �� �	#CC4682B46O   �� 5�  2    � ��
 �����6O   �� 5�  2    � ��$
 Bold"�6O   �� 5�  2    � ��$ 32��6K   �� 5�  2    � ��$ Center=�6_   5�  /   5�  +   1 5�  *   
  5�  .   $2 
IP Address��6A   $	3 80*	 6U   6 6a   �� *IP+7 5�  2   �� 5�  6   " ��6<   .�5�  :   ��  � ��$ Center=�6^   �� 5�  :    � ��$ 25��6S   �� 5�  :    � ��$ Center=�6`   5�  7   5�  /     5�  .   $2 
Percentage��6E   $
3 100*	 6Y   4 . 5 5�  2   �� 5�  6   .R�5�  :   �� $ 
Horizontal=�6E   $��Center=�6^   .+�5�  >   �� $
��Fill=�6�   $	��200��6�   $��25��6�   ��6D   �� *	ImgPath+; �� 	Converter#; [ 6�   �� 5�  >   $��30, 0, 0, 0q�6H   $��40��6]   $��Center=�6h   ��6�   �� *
Percentage+5�  ;   5�  7   5�  /     5�  .   $2 Number��6E   $
3 100*	 6U   4 . 5 5�  2   �� 5�  6   .R�5�  :   �� $ 
Horizontal=�6E   $��Center=�6^   .+�5�  >   �� $
��Fill=�6�   $	��200��6�   $��25��6�   ��6D   �� *NumberImgPath+#; [ 6�   �� 5�  >   $��30, 0, 0, 0q�6H   $��60��6]   $��Center=�6h   ��6�   �� *Rate+5�  ;   5�  7   5�  /   5�  '   5�  #   5�     5      5     5     5     �� 5     #���  6   ��6K   Z� ��5     �� 5	     $ Patch��6   $	����6,   $��Center=�6?   $2��,pack://application:,,,/Resource/#FontAwesome)�5
      
�������$��Center=�5      $��35(�6;   $��	-4,0,-3,0q�6J   .��5     � ���#303030.+�5     �� $��	0,-10,0,0q�6!   
������� 5     ��%J  '��       �� 5     " ��6$   .�5  "   ��  � �� 5  &   �� 5  *   " ��6:   .R�5  .   � $��Panel��63   .+�5  2   �� $��Border��69   $��1,1,1,1q�6H   
�������$ 2��6{   �� 5  2   $ ContentSite��6C   $��Center=�6Y   $��Center=�6u   $��Header��6�   $
��10,2q�6�   5  /    5  .   P� 5  2    �@��
IsSelected . . �5  6   �� $  Panel��6=    � �� �#e976006f   5  3   P� 5  2    �@��
IsSelected .  . �5  6   �� $  Panel��6=    � �� �#3030306f   5  3   5   +   5"  #   5#     5$     .@�5&     �� $��40��6"   $��-1,9,0,0q�6.   ���#e97600��5'     �� 5(  "   $ 
Horizontal=�6-   $	��100��6F   .+�5)  &   �� $��
Auto Patch��6,   $��	0,-10,0,0q�6A   $��Center=�5*      $��Center=�6D   
�������$��16(�5+      $��99��6.   $��31��69   5,  #   .��5.     � $��2��6#    50  "   �� 51  &   $	��320 �67   �� 52  &   $	��40* �67   �� 53  &   $	��40* �67   .+�55  "   � $��0��6'   $��2��67    56  &   �� 57  *   $��40 �68   �� 58  *   $	��50* �68   �� 59  *   $��40 �68   �� 5:  *   $	��50* �68   .+�5<  &   �� -   5        $ ClientPatchStatus��5<  ,   $��0��6G   $��Started Time :��6T   ���	#FF636363$��14(�6�   $��1q�6�   $��Bottom=�6�   $��SemiBold"�6�   $��42��6�   $	��300��6�   $��0,0,0,0q�6�   �� 5=  &   -   5        $ ServerPatchStatus��5=  ,   $��2��6G   $��Started Time:��6T   ���	#FF636363$��14(�6�   $��1q�6�   $��Bottom=�6�   $��SemiBold"�6�   $��40��6�   $	��300��6�   $��0,0,0,0q�6�   �� 5?  &   -   5        < ��Name$< ClientBorder��5?  -   $	��300��6C   $��	10,0,0,20q�6O   $��1��6b   $
��Left=�6p   ���	#FF5B5B5B$��1q�6�   ._�5@  *   � $��0��6/   $��10,10,10,10q�6<    5B  .   �� 5C  2   $
��0.6* �6C   �� 5D  2   $��1* �6C    5H  .   �� 5I  2   $��3* �6@   �� 5J  2   $��3* �6@   �� 5K  2   $��1* �6@   �� 5L  2   $��3* �6@   �� 5M  2   $��1* �6@   �� 5N  2   $��3* �6@   .+�5P  .   �� $��Client Information��64   ���	#FF636363$��0��6i   $��0��6v   $��16(�6�   $��1q�6�   $
��Left=�6�   $��Center=�6�   $��SemiBold"�6�   $��40��6�   $��0,-1,131,37q�6   $��2��6  $��2��6)  �� 5Q  .   $��Version��64   ���	#FF636363$��0��6^   $��16(�6n   $��1q�6|   $��Center=�6�   $
��Left=�6�   $��SemiBold"�6�   $��38��6�   $��1��6�   �� 5T  .   -   5        = ��Name$= ClientVersion��5T  6   $��1��6M   $��37��6]   $��Center=�6j   $��1��6�   $��14(�6�     SelectFileTextBoxStyle#���  6�   $��Center=�6�   $��0,1,0,0q�6  $��1��6(  {�6�   �� *IsAvailable+6(  �� 5U  .   $��0��64   $��File Browser��6D   ���	#FF636363$��16(�6r   $��1q�6�   $��Center=�6�   $��SemiBold"�6�   $��38��6�   $��3��6�   �� 5V  .   -   5        $= 
ClientPath��5V  6   $��3��6K   $��1��6X   $��Center=�6h   #���  6�   ���	#FFFFFFFF���	#FFABADB3$��1q�6�   $��14(�6   
���   �$��1��6#  $��35��6R  $��0,0,39,0q�6^  {�60  �� *IsAvailable+6^  �� 5W  .   -   5        $ ClientFileButton��5W  B   $��3��65   $��1��6{   $��	131,0,0,0q�6�   $��Right=�6�   $	��...��6�   $	��1 0q�6�   $��14(�6�   
����������	#FFD6D6D6���	#FFD6D6D6" .   SelectFileButtonStyle#���  63  $��35��6c  $��35��6o  $ 0.5,0.5&�6z  �� 5X  .   -   5        $ 	ClietnSet��5X  B   $��5��65   $��Center=�6m   $��Center=�6�   $��0��6�    	 RoundedSettingsButtonStyle#��� 	 6�   $	��Set��6�   $
��Bold"�6  
����������	#FF40A6D1���	#FF40A6D1$��1q�6]  $��35��6q  $��16(�6~  $��	29,0,0,10q�6�  �� 5Y  .   -   5        $ ClietnCancel��5Y  B   $��5��65   $��1��6s   $��Center=�6�   $��Center=�6�   #��� 	 6�   $��Cancel��6�   $
��Bold"�6  
����������	#FF40A6D1���	#FF40A6D1$��1q�6f  $��35��6z  $��16(�6�  $��
59,0,36,10q�6�  5Z  +   5[  '   �� 5]  &   -    5        $< ServerBorder��5]  �   $��	10,0,0,10q�6-   $	��300��6@   $��3��6L   $
��Left=�6Y   ���	#FF5B5B5B$��1q�6�   
�������._�5^  *   � $��0��6/   $��10,10,10,10q�6<    5_  .   �� 5`  2   $
��0.6* �6C   �� 5a  2   $��1* �6C    5d  .   �� 5e  2   $��3* �6@   �� 5f  2   $��3* �6@   �� 5g  2   $��1* �6@   �� 5h  2   $��3* �6@   �� 5i  2   $��1* �6@   �� 5j  2   $��3* �6@   .+�5l  .   �� $��Server Information��64   ���	#FF636363$��0��6i   $��0��6v   $��16(�6�   $��1q�6�   $��Center=�6�   $��SemiBold"�6�   $��40��6�   $��0,-3,111,31q�6�   $��2��6�   $��2��6  �� 5m  .   $��Version��64   ���	#FF636363$��0��6^   $��16(�6n   $��1q�6|   $��Center=�6�   $��SemiBold"�6�   $��34��6�   $��1��6�   �� 5n  .   -!   5        $= ServerVersion��5n  6   $��1��6M   $��35��6]   $��Center=�6j   $��1��6�   $��14(�6�   $��Center=�6�   $��1��6�   {�6�   �� *IsAvailable+6�   �� 5p  .   $��0��64   $��File Browser��6D   ���	#FF636363$��16(�6r   $��1q�6�   $��Center=�6�   $��SemiBold"�6�   $��34��6�   $��3��6�   �� 5q  .   -"   5        $= 
ServerPath��5q  6   $��0,0,36,0q�6J   $��3��6\   $��1��6i   $��Center=�6y   #���  6�   ���	#FFFFFFFF���	#FFABADB3$��1q�6�   $��14(�6  
���   �$��1��64  $��35��6c  {�6A  �� *IsAvailable+6c  �� 5r  .   -#   5        $ ServerFileButton��5r  B   $��3��65   $��1��6{   $��	130,0,0,0q�6�   $��Right=�6�   $	��...��6�   $	��1 0q�6�   $��14(�6�   
����������	#FFD6D6D6���	#FFD6D6D6" . #���  63  $��35��6c  $��35��6o  $ 0.5,0.5&�6z  �� 5s  .   -$   5        $ 	ServerSet��5s  B   $��5��65   $��Center=�6m   $��Center=�6�   $��0��6�   #��� 	 6�   $	��Set��6�   $
��Bold"�6  
����������	#FF40A6D1���	#FF40A6D1$��1q�6]  $��35��6q  $��16(�6~  $��	29,0,0,10q�6�  �� 5t  .   -%   5        $ ServerCancel��5t  B   $��5��65   $��1��6s   $��Center=�6�   $��Center=�6�   #��� 	 6�   $��Cancel��6�   $
��Bold"�6  
����������	#FF40A6D1���	#FF40A6D1$��1q�6f  $��35��6z  $��16(�6�  $��
59,0,36,10q�6�  5u  +   5v  '   5w  #   � 5x  "   $��1��6'   $��2��67    5y  &   �� 5z  *   $��40 �68   �� 5{  *   $	��50* �68   .+�5}  &   �� -&   5        $ ClientPatchResult��5}  ,   $��0��6G   $��Client Patch Result : ��6T   ���	#FF636363$��16(�6�   $��1q�6�   $
��Left=�6�   $��Center=�6�   $��SemiBold"�6�   $��32��6�   $��1��6  $��0,0,0,0q�6    5  &   -'   5        $% ClientPatchGrid��5  /   $��Visible=�6H   $& FullRow=�6|   $' Single=�6�   $��1��6�   $
( None=�6�   ) . $* Flase��6�   + �	#FFe7f4fc, . - . . . ���	#FF676868$��	0,0,10,10q�6�  $��14(�6�  / 5�  *   �� 5�  .   "  64   .�5�  2   ��  � ��$ 1,0,0,1q�6T   �� 5�  2    � ��
 ��ح�6P   5�  /    5�  *   > �� Triggers��%�  '��       '  �     7 �� 5�  .   " ��64   > 5�  2   P� 5�  6    �@��Orientation$ Vertical=�6U   . �5�  :   ��  � ��$ 5��6R   5�  7   5�  /   �� 5�  .   80   "  6m   .�5�  2   ��  � �� �	#CC4682B46O   �� 5�  2    � ��
 �����6O   �� 5�  2    � ��$
 Bold"�6O   �� 5�  2    � ��$ 32��6K   �� 5�  2    � ��$ Center=�6_   5�  /   5�  +   1 5�  *   
  5�  .   $2 IP��6A   $	3 70*	 6M   6 6Y   �� *UserIP+7 5�  2   �� 5�  6   " ��6<   .�5�  :   ��  � ��$ Center=�6^   �� 5�  :    � ��$ 25��6S   �� 5�  :    � ��$ Center=�6`   5�  7   5�  /   
  5�  .   $2 Version��6A   $	3 30*	 6R   6 6^   �� *Status+7 5�  2   �� 5�  6   " ��6<   .�5�  :   ��  � ��$ Center=�6^   �� 5�  :    � ��$ 25��6S   �� 5�  :    � ��$ Center=�6`   5�  7   5�  /   
  5�  .   $
2 Time��6A   $
3 100*	 6O   6 6\   �� *	CreatedAt+7 5�  2   �� 5�  6   " ��6<   .�5�  :   ��  � ��$ Center=�6^   �� 5�  :    � ��$ 25��6S   �� 5�  :    � ��$ Center=�6`   5�  7   5�  /   5�  '   5�  #   � 5�  "   $��2��6'   $��2��67    5�  &   �� 5�  *   $��40 �68   �� 5�  *   $	��50* �68   .+�5�  &   �� -(   5        $ ServerPatchResult��5�  ,   $��0��6G   $��Server Patch Result : ��6T   ���	#FF636363$��16(�6�   $��1q�6�   $
��Left=�6�   $��Center=�6�   $��SemiBold"�6�   $��32��6�   $��2��6  $��0,0,0,0q�6    5�  &   -)   5        $% ServerPatchGrid��5�  /   $��Visible=�6H   $& FullRow=�6{   $' Single=�6�   $��1��6�   $
( None=�6�   ) . $* Flase��6�   + �	#FFe7f4fc, . - . . . ���	#FF676868$��	0,0,10,10q�6�  $��14(�6�  / 5�  *   �� 5�  .   "  64   .�5�  2   ��  � ��$ 1,0,0,1q�6T   �� 5�  2    � ��
 ��ح�6P   5�  /    5�  *   ��%�  '��       '  �     7 �� 5�  .   " ��64   > 5�  2   P� 5�  6    �@��Orientation$ Vertical=�6U   . �5�  :   ��  � ��$ 5��6R   5�  7   5�  /   �� 5�  .   80   "  6m   .�5�  2   ��  � �� �	#CC4682B46O   �� 5�  2    � ��
 �����6O   �� 5�  2    � ��$
 Bold"�6O   �� 5�  2    � ��$ 32��6K   �� 5�  2    � ��$ Center=�6_   5�  /   5�  +   1 5�  *   
  5�  .   $2 IP��6A   $	3 70*	 6M   6 6Y   �� *UserIP+7 5�  2   �� 5�  6   " ��6<   .�5�  :   ��  � ��$ Center=�6^   �� 5�  :    � ��$ 25��6S   �� 5�  :    � ��$ Center=�6`   5�  7   5�  /   
  5�  .   $2 Version��6A   $	3 30*	 6R   6 6^   �� *Status+7 5�  2   �� 5�  6   " ��6<   .�5�  :   ��  � ��$ Center=�6^   �� 5�  :    � ��$ 25��6S   �� 5�  :    � ��$ Center=�6`   5�  7   5�  /   
  5�  .   $
2 Time��6A   $
3 100*	 6O   6 6\   �� *	CreatedAt+7 5�  2   �� 5�  6   " ��6<   .�5�  :   ��  � ��$ Center=�6^   �� 5�  :    � ��$ 25��6S   �� 5�  :    � ��$ Center=�6`   5   7   5  /   5  '   5  #   5
     5     �� 5     $��40��6"   $��-1,9,0,0q�6.   ���#e97600��5     �� 5  "   $ 
Horizontal=�6-   $	��200��6F   .+�5  &   �� $��Condition Zero Patch��6,   $��	0,-10,0,0q�6K   $��Center=�5      $��Center=�6E   $��16(�5      $	��201��6.   
�������6:   5  #   .��5     � $��
0,20,10,20q�6#   $��0��67   $��2��6D    5  "   �� 5  &   $
��1.5* �67   �� 5  &   $��1* �67   .+�5  "   � $��0��6'   $��0,0,0,0q�67    5  &   �� 5  *   .+�5   &     -*   5        $% 	ValidList��5   K   , .  $��Center=�6_   ?   CanUserAddRows? . $��
20,45,20,0q�6�   
���   ���6�   �� @ �� Mode$@ OneWay=�1 5!  *   4  .System.Windows.Controls.DataGridCheckBoxColumn  5"  .   4 .  6 6E   �� *	IsChecked+$@ TwoWay=�A �� UpdateSourceTrigger$A PropertyChanged=�8 5#  2   �� 5$  6   .R�5%  :   �� $��
All Select��6C   $��2q�6Z   5&  7   5(  /     5*  .   $2 Status��6E   4 . $
3 0.5*	 6h   B   HeaderStyleB 5+  2   �� 5,  6   "  6<   .�5-  :   ��  � ��$ Center=�6g   5.  7   5 50  2   �� 51  6   .R�52  :   �� $��15��6@   $��15��6K   ��6W   �� *	IconImage+53  7   55  /   
  56  .   $2 
IP Address��6A   4 . $3 2*	 6�   6 6g   �� *	IPAddress+B 57  2   �� 58  6   "  6<   .�59  :   ��  � ��$ Center=�6g   5:  7   7 5<  2   �� 5=  6   " ��6<   .�5>  :   ��  � ��$ Center=�6`   5?  7   5A  /   
  5B  .   $2 Duration��6A   4 . $3 1*	 6�   6 6e   �� *During+B 5C  2   �� 5D  6   "  6<   .�5E  :   ��  � ��$ Center=�6g   5F  7   7 5H  2   �� 5I  6   " ��6<   .�5J  :   ��  � ��$ Center=�6`   5K  7   5M  /   
  5N  .   $2 Result��6A   4 . $3 1*	 6   6 6d   �� *Status+B 5O  2   �� 5P  6   "  6<   .�5Q  :   ��  � ��$ Center=�6g   5R  7   7 5T  2   �� 5U  6   " ��6<   .�5V  :   ��  � ��$ Center=�6`   5W  7   5Y  /   5\  '   5]  #   �� 5^  "   $��1��6)   $��	0,45,10,0q�69   
���   �$��1q�6`   ._�5_  &   � $��1��6+   $��1��6?   ���	#FFF3F3F3 5f  *   �� 5g  .   $��1* �6?   �� 5h  .   $��2* �6?   �� 5i  .   $
��0.3* �6?    5k  *   �� 5l  .   $	��20* �6<   �� 5m  .   $��40 �6<   �� 5n  .   $��20 �6<   �� 5o  .   $��40 �6<   �� 5p  .   $��25 �6<   �� 5q  .   $	��100 �6<   �� 5r  .   $��20 �6<   �� 5s  .   $��40 �6<   �� 5t  .   $��20 �6<   �� 5u  .   $��40 �6<   �� 5v  .   $��20 �6<   �� 5w  .   $��40 �6<   �� 5x  .   $��15 �6<   �� 5y  .   $��40 �6<   �� 5z  .   $	��20* �6<   .+�5}  *   �� -+   5        $= StartIP��5}  2   $��1��6D   $��2��6Q   $��1��6e   $��0,0,10,0q�6u   $��10q�6�   $��Center=�5~  *     
 RoundedGlowingIPTextBoxStyle#��� 
 6L   $ Start IP��5  *   $��1��6=   $��14(�6J   {�6X   �� *IsAvailable+�� 5�  *   $��
Start IP :��60   $��Right=�6E   $��0,5,5,0q�6a   $	��Top=�6r   $��1��6�   ���	#FF464646$��20(�6�   $��Sitka Small)�6�   $	��104��6�   $��31��6�   
������ 6�   �� 5�  *   -,   5        $= EndIP��5�  2   $��1��6A   $��2��6S   $��3��6g   $��0,0,10,0q�6t   $��10q�6�   $��Center=�5�  *   #��� 
 6L   $ End IP��5�  *   $��1��6;   $��14(�6H   {�6V   �� *IsAvailable+�� 5�  *   $��End IP :��60   $��Right=�6D   $	��Top=�6`   ���	#FF464646$��20(�6�   $��Sitka Small)�6�   $��92��6�   $��32��6�   $��3��6�   $��0,5,5,0q�6�   $ 0.48,3.9&�6�   �� 5�  *   $��0��63   $��Options��6C   $��68��6T   $��
10,33,10,0q�6`   $	��Top=�6t   $��3��6�   $��5��6�   ���	#FF92D6FF$��Segoe UI Black)�6�   $��20(�6�   ���	#FF3F778F.��5�  .   �� $��
105,10,0,0q�69   $ 
Horizontal=�6M   .+�5�  2   	� --   5        C 	�Name$
C Ping��5�  R   D 	� 	GroupName$D Options��6>   $��Condition Ping��6`   E d� 	IsChecked$
E TrueY�6y   $��Segoe UI)�6�   $��12(�6�   $��18,0,0,0q�6�   6�   	� 5�  2   -.   5        $
C Zero��5�  R   $D Options��6>   $��Condition Zero��6`   $��Segoe UI)�6y   $��12(�6�   $��28,0,0,0q�6�   6�   5�  /   5�  +   �� 5�  *   -/   5        $= Version��5�  2   $��2��6C   $��7��6W   $��0,0,10,0q�6d   $��1��6v   $��10q�6�   $��Center=�5�  *   #��� 
 6L   $ Version��5�  *   $��1��6<   $��14(�6I   {�. 6W   �� 5�  *   $��	Version :��60   $��Right=�6D   $	��Top=�6`   ���	#FF464646$��20(�6�   $��Sitka Small)�6�   $	��109��6�   $��34��6�   $��7��6�   $��0,3,0,0q�6�   �� 5�  *   -0   5        $
! Port��5�  3   " .  $��2��6T   $��9��6h   $��0,0,10,0q�6u   $��1��6�   $��Center=�5�  *   $ Width��5�  *   $��1��6:   $��14(�6G   # . $$ -1��6g   .@�5�  .   �� $��Client��6;   �� 5�  .   $��Server��6;   5�  +   �� 5�  *   $��Type :��60   $��Right=�6A   $��0,4,0,0q�6]   $	��Top=�6n   ���	#FF464646$��20(�6�   $��Sitka Small)�6�   $��79��6�   $��32��6�   $��9��6�   �� 5�  *   -1   5        $= 	PatchFile��5�  2   {�.  $��11��6Z   $
��10,0q�6h   $��2��6v   $��10q�6�   $��Center=�5�  *   #��� 
 6L   $ Height��5�  *   $��1��6;   $��14(�6H   �� 5�  *   -2   5        $ PatchBrowserFile��5�  i   $	��...��5�  2   $��14(�6A   ���	#FF40A6D1$��1��6f   $��11��6v   ���	#FFD6D6D6���	#FF8DDAFB" . #���  5�  )   $��
222,0,10,0q�6Z   $��2��6n   �� 5�  *   -3   5        $ CheckButton��5�  1   $
��Ping��6F   $��103,5,179,0q�6U   $��13��6j   #���  5�  )   $��SemiBold"�5�  )   
����������	#FFE8850E$��14(�5�  @   $��3��6O   $��85��6\   $ 1.174,0.864&�6g   $��Center=�6�   $��2��6�   ��5�  R   Z� 5�  �   �� 5�  *   -4   5        $ PatchButton��5�  1   $��Patch��6F   $��
108,5,52,0q�6V   $��13��6j     RoundedButtonStyle#���  5�  )   $��SemiBold"�5�  )   
����������	#FF40A6D1$��14(�5�  @   $��3��6O   $��1��6\   ��5�  R   Z� 5�  \   5�  '   5�  #   5�     5�     5�     5�     5�     �� 5�     -5   5        #���  5�     ��6K   Z� ��5�     �� 5�     .+�5�     �� -6   5        $< message_body��5�  !   $��1��67   $��Right=�6G   $��0,3,2,0q�6d   ���	#FF303030$ 50��6�   $��15��6�   $��15��6�   �� 5�     -7   5        $	 USB��5�      $��0, -10, 0, 5q�6-   $��0��6C   $��44��6S   $��44��6_   $ 1��6k   $��/Resource/USB_Device.png��6|   $��Uniform=�6�   $ 0.396,1.005&�6�   $	��Top=�6�   5�     5�     .��5�     �  5�     �� 5�     $	��10* �6(   �� 5�     $��1* �6(    5�     �� 5�     $��* �6+   .+�5�     � $��0��6   $��0��6+    5�     �� 5�     $��* �6,   .+�5�     � $��0��6   $��5,5,5,5q�6,   .+�5�     � .+�5�  "     -8   5        $9 datePickerUSB��5�  -   $	��300��6D   $��33��6P   $��17(�6\   $��Right=�6j   $	��Top=�6�   $
: Long=�6�   |�.  $��	0,10,0,10q�6�   5�       5�     -9   5        $% USBDataGrid��5�  '   $& FullRow=�6<   $' Single=�6T   $
( None=�6l   ) . $
* true��6�   + �	#FFe7f4fc, . - . . . ���	#FF676868$��0,50,0,0q�6?  $��14(�6Q  / 5�  "   �� 5�  &   "  6,   .�5�  *   ��  � ��$ 1,0,0,1q�6L   �� 5�  *    � ��
 ��ح�6H   5�  '    5�  "   ��%�   '        7 �� 5�  &   80   "  6e   .�5�  *   ��  � �� �	#CC4682B46G   �� 5�  *    � ��
 �����6G   �� 5�  *    � ��$
 Bold"�6G   �� 5�  *    � ��$ 32��6C   �� 5�  *    � ��$ Center=�6W   5�  '   5�  #   1 5�  "   
  5�  &   $2 No��69   $3 30	 6E   6 6P   �� *No+7 5�  *   �� 5�  .   " ��64   .�5�  2   ��  � ��$ Center=�6V   �� 5�  2    � ��$ 25��6K   �� 5�  2    � ��$ Center=�6X   5�  /   5�  '     5
  &   $2 USB Type��6=   $	3 140	 6O   4 . 5 5  *   �� 5  .   .R�5  2   �� $ 
Horizontal=�6=   .+�5  6   �� $��Center=�6<   $��Uniform=�6�   $��28��6�   $��22��6�   ��6Y   �� *	ImagePath+#; [ 6�   �� 5  6   $��10, 0, 0, 0q�6@   $	��120��6U   $��Right=�6a   ��6}   �� *	UsbType+5  3   5  /   5  '   
  5  &   $2 IP��69   $	3 130	 6E   6 6Q   �� *UserIP+7 5  *   �� 5  .   " ��64   .�5  2   ��  � ��$ Center=�6V   �� 5  2    � ��$ 25��6K   �� 5  2    � ��$ Center=�6X   5  /   5  '   
  5  &   $2 Status��69   $3 80	 6I   6 6T   �� *Status+7 5   *   �� 5!  .   " ��64   .�5"  2   ��  � ��$ Center=�6V   �� 5#  2    � ��$ 25��6K   �� 5$  2    � ��$ Center=�6X   5%  /   5'  '   
  5(  &   $2 ID��69   $	3 80*	 6E   F   
Visibility$F Hidden=�6j   6 6Q   �� *Guid+7 5)  *   �� 5*  .   " ��64   .�5+  2   ��  � ��$ Center=�6V   �� 5,  2    � ��$ 25��6K   �� 5-  2    � ��$ Center=�6X   5.  /   50  '   
  53  &   $2 Device Name��69   $
3 100*	 6N   6 6[   �� *Device+7 54  *   �� 55  .   " ��64   .�56  2   ��  � ��$ Center=�6V   �� 57  2    � ��$ 25��6K   �� 58  2    � ��$ Center=�6X   59  /   5;  '   
  5?  &   $
2 Time��69   $	3 150	 6G   6 6S   �� *	CreatedAt+7 5@  *   �� 5A  .   " ��64   .�5B  2   ��  � ��$ Center=�6V   �� 5C  2    � ��$ 25��6K   �� 5D  2    � ��$ Center=�6X   5E  /   5G  '   5J     5K     5L     � 5M     $��1��6    5N     �� 5O     $��1* �6/   �� 5P     $��1* �6/   .+�5R     �� -:   5        $ USBClearButton��5R  !   $��Clear��69   $��50��6I   #���  5S  )   $��SemiBold"�5T  )   
����������	#FFE8850E$��14(�5U  @   $��3��6O   $	��150��6\   $ 1.174,0.864&�6h   $��Center=�6�   ��5T  R   Z� 5U  �   �� 5V     -;   5        $ USBAllClearButton��5V  !   $��	All Clear��6<   $��50��6P   #���  5W  )   $��SemiBold"�5X  )   
����������	#FF40A6D1$��14(�5Y  @   $	��150��6N   $��3��6[   $��1��6h   ��5X  R   Z� 5Y  h   5[     5\     5]     5_     5a     5b     