   M S B A M L   `   `   `  ����N  JMonitor.TaskControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Monitor.TaskControl.GlobalsMonitor.TaskControl.Globals  5   	   K*clr-namespace:Monitor.TaskControl.ResourceMonitor.TaskControl.Resource  5   	   C&clr-namespace:Monitor.TaskControl.ViewMonitor.TaskControl.View  5   	   .    (Monitor.TaskControl.View.CustomMsgWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =globals)clr-namespace:Monitor.TaskControl.Globals     5   	   ?controls*clr-namespace:Monitor.TaskControl.Resource     5   	   8local&clr-namespace:Monitor.TaskControl.View     5   	   -   5        $	��200��5
   	   $	��300��6     � 
ResizeMode$  NoResize=�5   	   $	��350��5   
   $	��210��6    � WindowStartupLocation$ CenterScreen=�5   	   
���   � � Topmost . .��5      �  � ColumnDefinitions 5   
   �� 5      $
��221* �6   �� 5      $
��123* �6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #eeeeee��6    � Offset$ 0Q�6/   � 5      $ #c3c3c3��6   $ 1Q�6/   � 5      $ #eeeeee��6   $ 0.866Q�6/   5       � RowDefinitions 5   
   �� 5       $��0 �6   �� 5!      .+�5#   
   � -   5        $��30��5#      $	��Top=�6   ���	#FF40A6D1$��1,0,0,0q�6e   $��2��6v   $��2��6�   	 J� Effect	 5$      3  -System.Windows.Media.Effects.DropShadowEffect  5%      
   
BlurRadius$
 10Q�6#      ShadowDepth$ 3Q�63      	Direction$	 270Q�6C      Opacity$	 0.2Q�6S    5'      �� 5(      $
��Auto �6#   �� 5)      �� 5*      $
��Auto �6#   .+�5.      3   -Monitor.TaskControl.Resource.HeaderDecoration  $��2��6(   $��
-1,0,132,0q�6<   ]� 50      $��2��6   $��0��6,   $
��Left=�6<   ��.  .+�52      �� $��	 Warning!��6   $��Center=�6,   $��Bottom=�6N   
�������$��SemiBold"�53      $��14(�6.    J� IsManipulationEnabled . $��0,5,5,5q�6Y   $��	7,-1,0,-3q�6k   $��34��6~   $	��112��6�   54      �� 55      $��1��6   $��2��6-   $��Center=�6=    �� Orientation$ 
Horizontal=�6X   $��0,0,0,0q�56      .+�58      �� -   5        $��30��58      $��30��6$   $	����60      CloseMinMaxButtonStyle#���   6<   
������ 
������ 
�������$2��,pack://application:,,,/Resource/#FontAwesome)�5:      $��20(�6S   $��Bottom=�5;      |�.  6W   5<      5=      � 5>   
   $��0��6   $��35��6   $��Stretch=�6+   $��Stretch=�6I   $��
0,33,0,113q�6e   $��1��6y   $��2��6�    5?      �� 5@      $��* �6#    5B      �� 5C      $
��Auto �6    $��35��6.   5E      � 5F   
   $��0��6   $��Stretch=�6   $��Stretch=�6=   $��	0,63,0,68q�6Y   $��1��6l   $��2��6y    5G      �� 5H      $��* �6#    5J      �� 5K      $
��Auto �6    $��35��6.   .+�5M      �� -   5         ��Name$ btn_Msg��5M      $��Stretch=�6&   $��0,5,5,0q�6D   $	��0 2q�6U   ���#eeeeee
�������$��0q�5O   9    � FocusVisualStyle 5P      �� 5Q      .�5R      ��  �� Property � �� �� Value 5S      �� 5T   "   .R�5U   &   �� $��2q�60    J� SnapsToDevicePixels . #��� ��6V   $��1��6�    �� StrokeDashArray$	 1 2R�6�   5V   #   5X      5Y      .��5[      �� $ 
Horizontal=�6   .+�5\      �� $��25,9,9,6q�6   $��29��6.   $��/Resource/Warning.ico��69   $
��Fill=�6X   $��29��6g   �� 5]      -   5         ��Name$ lbl_Msg��5]      $#��You are missing the anything.��6-   $��0,9,10,6q�6U   $��Center=�6g   $	��Top=�6�   $��Sitka Small)�6�   $��16(�6�   $	��259��6�    J� RenderTransformOrigin$ 
0.15,0.471&�6�   $��30��6�   5^      5_      5`      � 5a   
   $��0��6   $��Stretch=�6   $��Stretch=�6=   $��
0,134,0,10q�6Y   $��1��6m   $��2��6z    5b      �� 5c      $��* �6#    5e      �� 5f      $
��Auto �6    $��30��6.   .+�5h      �� -   5        $ LoginButton��5h      $��OK��6*     RoundedButtonStyle#���  67   $
��25 5q�5i      $��SemiBold"�6(   
����������	#FF40A6D1$��14(�5j   0   $��3��6W   $	��100��6d   $ 1.174,0.864&�6p   $��30��6�   $��Center=�6�   ��5i   Q   Z� 5j   �   5k      � 5l   
   $��0��6   $��Stretch=�6   $��Stretch=�6=   $��	0,149,0,1q�6Y   $��1��6l   $��2��6y    5m      �� 5n      $��* �6#    5p      �� 5q      $
��Auto �6    $��30��6.   5s      5t      5u      