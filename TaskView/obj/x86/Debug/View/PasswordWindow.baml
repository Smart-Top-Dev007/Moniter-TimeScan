   M S B A M L   `   `   `  ����K  GMonitor.TaskView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskView.GlobalsMonitor.TaskView.Globals  5   	   E'clr-namespace:Monitor.TaskView.ResourceMonitor.TaskView.Resource  5   	   =#clr-namespace:Monitor.TaskView.ViewMonitor.TaskView.View  5   	   *    $Monitor.TaskView.View.PasswordWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :globals&clr-namespace:Monitor.TaskView.Globals     5   	   <controls'clr-namespace:Monitor.TaskView.Resource     5   	   5local#clr-namespace:Monitor.TaskView.View     5   	   -   5        $	��200��5
   	   $	��300��6     � 
ResizeMode$  NoResize=�5   	   $	��300��5   
   $	��229��6    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$7 1/Monitor.TaskView;component/Resource/TaskView.ico��5   	   
���   �.��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #eeeeee��6    � Offset$ 0Q�6/   � 5      $ #c3c3c3��6   $ 1Q�6/   � 5      $ #eeeeee��6   $ 0.866Q�6/   5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      .+�5   
   � -   5        $��50��5      $	��Top=�6   ���	#FF40A6D1 J� Effect 5       3  -System.Windows.Media.Effects.DropShadowEffect  5!      	   
BlurRadius$	 10Q�6#   
   ShadowDepth$
 3Q�63      	Direction$	 270Q�6C      Opacity$	 0.2Q�6S    � ColumnDefinitions 5#      �� 5$      $
��Auto �6#   �� 5%      �� 5&      $
��Auto �6#   �� 5'      $��60 �6#   .+�5*      0   *Monitor.TaskView.Resource.HeaderDecoration  ]� 5,      $��2��6   $��0��6,   $
��Left=�6<   $��50��6W   ��.  .+�5-      �� -   5         ��Name$ AVARTA��5-      $��25��6(   $��25��64    K� ZIndex$ 1��6?   $
��Left=�6P   $��	14,0,0,10q�6k   $��/Resource/net-on.png��6~   $��Uniform=�6�    J� RenderTransformOrigin$ 0.396,1.005&�6�   $��Bottom=�6�   �� 5/      $�� Password of Server��6   $��Center=�66   $��Center=�6X   
�������$��SemiBold"�50      $��21(�6.    J� IsManipulationEnabled . $��0,5,5,5q�6Y   $	��0,6q�6k   51      �� 52      $��2��6   $��2��6-   $��Center=�6=    �� Orientation$ 
Horizontal=�6X   $��0,0,10,0q�53      .+�5>      �� -   5        $��40��5>      $��40��6$   $��20,0,0,0q�60   $	����6B      CloseMinMaxButtonStyle#���   6N   
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�5@      $��20(�6O   $��Bottom=�5A      |�.  6W   5B      5C      � 5E   
   $��0��6   $��1��6   $��Stretch=�6,   $��Stretch=�6J   $��	0,30,0,-4q�6f    5F      �� 5G      $��* �6#   �� 5H      $��212.714 �6#   �� 5I      $��61.286 �6#    5K      �� 5L      $
��Auto �6    �� 5M      $
��Auto �6    �� 5N      $
��Auto �6    �� 5O      $��* �6    .+�5R      ]� $��1��6   $��2��6(   $��1��6<   $��0,0,20,5q�6I   ��.  .+�5S      G� -   5         G�Name$ PasswordTextBox��5S      $	��Top=�67   $��35��6L   $��Center=�5T       " RoundedGlowingPasswordBoxStyle#���  6@    � ToolTip$ Password��5U      $��2��61   $ 0.524,-0.843&�6�   {�6`   �� *IsAvailable+6�   �� 5V      $
��Left=�6   $
��10px(�6.   
�������  LinkLabelStyle#���  5W      |�. $��5��6R   .��5X      �� $@��:You will need to ask your administrator for the password. ��6      LinkLabelTextBlockStyle#���  6b   $	��254��6�   $��9(�6�   5Y      �� 5Z      $
��Left=�6   $
��10px(�6.   
�������$��-110 20 0 0q�6U   #���  5[      |�. $��5��6R   5\      �� 5^      $��1��6   $��2��6)   $ Vertical=�66   $��0 0 20 0q�6M   $��2��6_   .+�5_      �� $ 
Horizontal=�6   $��0 0 0 0q�66   {�6G   �� *IsAvailable+.+�5`      �� -   5         ��Name$ RememberCheckBox��5`      $��Remember me��69   $��Center=�6O   $��0 0 50 0q�6j   
���iii�  RoundedCheckBoxStyle#���  5a   0   $��4��6_    d� 	IsChecked$
 TrueY�5b      �� 5c      -   5         ��Name$ LoginButton��5c      $��LOG IN��62     RoundedButtonStyle#���  6C   $
��25 5q�5d      $��SemiBold"�6(   
����������	#FF40A6D1$��14(�5e   0   $��3��6]   ��5d   Q   Z� 5e   ]   5f      5h      5j      5k      5l      