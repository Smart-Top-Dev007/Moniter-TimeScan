   M S B A M L   `   `   `  ����N  JMonitor.TaskControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskControl.ViewMonitor.TaskControl.View  5   	   K*clr-namespace:Monitor.TaskControl.ResourceMonitor.TaskControl.Resource  5   	   *    $Monitor.TaskControl.View.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Monitor.TaskControl.View     5   	   ?controls*clr-namespace:Monitor.TaskControl.Resource     5   	   -   5        $	��285��5	   	   $	��450��6     � 
ResizeMode$  NoResize=�5
   	   $��442.584��5   
   $	��250��6    � WindowStartupLocation$ CenterScreen=�5   	   
���   �.��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #eeeeee��6    � Offset$ 0Q�6/   � 5      $ #c3c3c3��6   $ 1Q�6/   � 5      $ #eeeeee��6   $ 0.866Q�6/   5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      .+�5   
   � -   5        $��50��5      $	��Top=�6   ���	#FF40A6D1 J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 10Q�6#   	   ShadowDepth$	 3Q�63   
   	Direction$	
 270Q�6C      Opacity$	 0.2Q�6S    � ColumnDefinitions 5!      �� 5"      $
��Auto �6#   �� 5#      �� 5$      $
��Auto �6#   �� 5%      $��60 �6#   .+�5(      3   -Monitor.TaskControl.Resource.HeaderDecoration  ]� 5*      $��2��6   $��0��6,   $
��Left=�6<   $��50��6W   ��.  .+�5+      �� -   5         ��Name$ AVARTA��5+      $��25��6(   $��25��64    K� ZIndex$ 1��6?   $
��Left=�6P   $��	14,0,0,10q�6k   $��/Resource/net-on.png��6~   $��Uniform=�6�    J� RenderTransformOrigin$ 0.396,1.005&�6�   $��Bottom=�6�   �� 5-      $�� Password of Server��6   $��Center=�66   $��Center=�6X   
�������$��SemiBold"�5.      $��21(�6.    J� IsManipulationEnabled . $��0,5,5,5q�6Y   $	��0,6q�6k   5/      �� 50      $��2��6   $��2��6-   $��Center=�6=    �� Orientation$ 
Horizontal=�6X   $��18,0,0,0q�51      $��50��6+   .+�53      �� -   5        $��39��53      $��40��6$   $	����60      CloseMinMaxButtonStyle#���   6<   
������ 
������ 
�������$2��,pack://application:,,,/Resource/#FontAwesome)�55      $��20(�6S   $��Bottom=�56      |�.  6W   57      58      � 5:   
   $��0��6   $��1��6   $��Stretch=�6,   $��Stretch=�6J   $��0,40,0,0q�6f   $ 0.499,0.602&�6x    5;      �� 5<      �� 5=      $	��228 �6#   �� 5>      $��61.286 �6#    5@      �� 5A      $
��Auto �6    �� 5B      $
��Auto �6    �� 5C      $
��Auto �6    �� 5D      $
��Auto �6    �� 5E      $
��Auto �6    �� 5F      $��* �6    .+�5I      � .+�5J      �� $��32,-10,50,31q�6   $��14(�6.   $��Directory :��6<   �� 5K      -   5         ��Name$ WorkDirectory��5K      $��Center=�61   $��105,-10,-224,31q�6S   $��33��6l     SelectFileTextBoxStyle#���  5L      ���	#FFFFFFFF���	#FFABADB3$��1q�5M   2   $��14(�6G   
���   � � ToolTip$ Work Directory��5N      $��1��6/   {�6s   �� *IsAvailable+�� 5O      $)��#Please select your work directory. ��6   
�������  LinkLabelTextBlockStyle#���  6]   $	��227��6�   $��12(�6�   $��118,27,-190,10q�6�   �� 5P      -   5        $��Right=�5P      $	��...��60   $	��1 0q�6>   $��14(�6L   
����������	#FFD6D6D6���	#FFD6D6D6 J� 	AllowDrop .   SelectFileButtonStyle#���  5R      $��379,-12,-256,31q�6s   5S      ]� 5U      $��1��6   $��2��6(   $��1��6<   $��0,0,20,5q�6I   ��.  .+�5V      �� $��76��6   $��14(�6#   $��
-180,6,0,0q�61   $��
Password :��6E   $��27��6Z   $	��Top=�6f   G� 5W      -   5         G�Name$ PasswordTextBox��5W      $	��Top=�67   $��33��6L   $��Center=�5X       " RoundedGlowingPasswordBoxStyle#���  6@   $ Password��5Y      $��2��61   $ 0.524,-0.843&�6�   $��
-57,0,16,0q�6�   {�6`   �� *IsAvailable+6�   �� 5Z      $
��Left=�6   $
��10px(�6.   
�������  LinkLabelStyle#���  5[      |�. $��5��6R   $	��260��6_   $ 0.412,0.611&�6k   .��5\      �� $3��-This password will be used all team members. ��6    $��	-44,0,0,0q�6U   #���  6h   $	��245��6�   $��12(�6�   $��18��6�   5]      �� 5^      $
��Left=�6   $
��10px(�6.   
�������$��-110 20 0 0q�6U   #���  5_      |�. $��5��6R   5`      �� 5b      $��1��6   $��2��6)   $ Vertical=�66   $��0 0 20 0q�6M   $��2��6_   .+�5c      �� $ 
Horizontal=�6   $��	-36,0,0,0q�66   $��27��6k   {�6I   �� *IsAvailable+.+�5d      �� -   5         ��Name$ RememberCheckBox��5d      $��I am a administrator��69   $��Center=�6X   $��
-85,1,50,1q�6s   
���iii�  RoundedCheckBoxStyle#���  5e   0   $��4��6_    d� 	IsChecked$ FalseY�5f      $	��159��6-   �� 5g      -	   5         ��Name$ LoginButton��5g      $��LOG IN��62     RoundedButtonStyle#���  6C   $
��25 5q�5h      $��SemiBold"�6(   
����������	#FF40A6D1$��14(�5i   0   $��3��6]   $��97��6j   $��28��6u   $��	75,0,0,-1q�6�   ��5h   Q   Z� 5i   �   5j      5l      5n      5o      5p      