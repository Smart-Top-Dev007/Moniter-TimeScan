   M S B A M L   `   `   `  ����K  GMonitor.TaskView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskView.GlobalsMonitor.TaskView.Globals  5   	   E'clr-namespace:Monitor.TaskView.ResourceMonitor.TaskView.Resource  5   	   =#clr-namespace:Monitor.TaskView.ViewMonitor.TaskView.View  5   	   +    %Monitor.TaskView.View.CustomMsgWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :globals&clr-namespace:Monitor.TaskView.Globals     5   	   <controls'clr-namespace:Monitor.TaskView.Resource     5   	   5local#clr-namespace:Monitor.TaskView.View     5   	   -   5        $	��200��5
   	   $	��300��6     � 
ResizeMode$  NoResize=�5   	   $	��350��5   
   $	��210��6    � WindowStartupLocation$ CenterScreen=�5   	   
���   �.��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #eeeeee��6    � Offset$ 0Q�6/   � 5      $ #c3c3c3��6   $ 1Q�6/   � 5      $ #eeeeee��6   $ 0.866Q�6/   5       � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      .+�5   
   � -   5        $��30��5      $	��Top=�6   ���	#FF40A6D1$��1,0,0,0q�6e   $��2��6v    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5          
BlurRadius$ 10Q�6#   	   ShadowDepth$	 3Q�63   
   	Direction$	
 270Q�6C      Opacity$	 0.2Q�6S    � ColumnDefinitions 5"      �� 5#      $
��Auto �6#   �� 5$      �� 5%      $
��Auto �6#   .+�5)      0   *Monitor.TaskView.Resource.HeaderDecoration  $��2��6(   $��
-1,0,132,0q�6<   ]� 5+      $��2��6   $��0��6,   $
��Left=�6<   ��.  .+�5-      �� $��	 Warning!��6   $��Center=�6,   $��Bottom=�6N   
�������$��SemiBold"�5.      $��14(�6.    J� IsManipulationEnabled . $��0,5,5,5q�6Y   $��	7,-1,0,-3q�6k   $��34��6~   $	��112��6�   5/      �� 50      $��1��6   $��2��6-   $��Center=�6=    �� Orientation$ 
Horizontal=�6X   $��0,0,0,0q�51      .+�53      �� -   5        $��30��53      $��30��6$   $��0,0,0,0q�60   $	����6A      CloseMinMaxButtonStyle#���   6M   
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�55      $��20(�6O   $��Bottom=�56      |�.  6W   57      58      � 59   
   $��0��6   $��35��6   $��Stretch=�6+   $��Stretch=�6I   $��0,0,0,80q�6e   $��1��6w    5:      �� 5;      $��* �6#    5=      �� 5>      $
��Auto �6    $��35��6.   5@      � 5A   
   $��0��6   $��Stretch=�6   $��Stretch=�6=   $��	0,63,0,68q�6Y   $��1��6l    5B      �� 5C      $��* �6#    5E      �� 5F      $
��Auto �6    $��35��6.   .+�5H      �� -   5         ��Name$ btn_Msg��5H      $��Stretch=�6&   $��0,5,5,0q�6D   $	��0 2q�6U     RoundedSettingsButtonStyle#���  5I   %   ���#eeeeee
�������$��0q�5J   O   ��68   Z� .��5K      �� $ 
Horizontal=�6   .+�5L      �� $��25,9,9,6q�6   $��29��6.   $��/Resource/Warning.ico��69   $
��Fill=�6X   $��29��6g   �� 5M      -   5         ��Name$ lbl_Msg��5M      $#��You are missing the anything.��6-   $��0,9,10,6q�6U   $��Center=�6g   $	��Top=�6�   $��Sitka Small)�6�   $��16(�6�   $	��253��6�    J� RenderTransformOrigin$ 
0.15,0.471&�6�   $��27��6�   5N      5O      5P      � 5Q   
   $��0��6   $��Stretch=�6   $��Stretch=�6=   $��
0,134,0,10q�6Y   $��1��6m    5R      �� 5S      $��* �6#    5U      �� 5V      $
��Auto �6    $��30��6.   .+�5X      �� -   5        $ LoginButton��5X      $��OK��6*     RoundedButtonStyle#���  67   $
��25 5q�5Y      $��SemiBold"�6(   
����������	#FF40A6D1$��14(�5Z   0   $��3��6W   $	��100��6d   $ 1.174,0.864&�6p   $��30��6�   $��Center=�6�   ��5Y   Q   Z� 5Z   �   5[      � 5\   
   $��0��6   $��Stretch=�6   $��Stretch=�6=   $��	0,149,0,1q�6Y   $��1��6l    5]      �� 5^      $��* �6#    5`      �� 5a      $
��Auto �6    $��30��6.   5c      5d      5e      