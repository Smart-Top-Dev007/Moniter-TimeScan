   M S B A M L   `   `   `  ����K  GMonitor.TaskView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskView.GlobalsMonitor.TaskView.Globals  5   	   E'clr-namespace:Monitor.TaskView.ResourceMonitor.TaskView.Resource  5   	   =#clr-namespace:Monitor.TaskView.ViewMonitor.TaskView.View  5   	   ,    &Monitor.TaskView.View.ConnectionWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :globals&clr-namespace:Monitor.TaskView.Globals     5   	   <controls'clr-namespace:Monitor.TaskView.Resource     5   	   5local#clr-namespace:Monitor.TaskView.View     5   	   -   5        $	��300��5   	   $	��600��6   
���   �  � WindowStartupLocation$  CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   $��1q�5   	   ���	#FFFAFAFA.��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #eeeeee��6    � Offset$ 0Q�6/   � 5      $ #c3c3c3��6   $ 1Q�6/   � 5      $ #eeeeee��6   $ 0.866Q�6/   5      .+�5   
   � -   5        ���	#FF40A6D1$��0,0,0,10q�5   A    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 10Q�6#   	   ShadowDepth$	 3Q�63   
   	Direction$	
 270Q�6C      Opacity$	 0.2Q�6S   .+�5      0   *Monitor.TaskView.Resource.HeaderDecoration  $��0,0,1,0q�6(   �� 5       -   5         ��Name$ AVARTA��5       $��23��6$   $��23��60    K� ZIndex$ 1��6;   $
��Left=�6L   $��12,0,0,7q�6g   $��/Resource/net-on.png��6y   $��Uniform=�6�    J� RenderTransformOrigin$ 0.396,1.005&�6�   $��Bottom=�6�   �� 5"      $ ��TaskView Connection Wizard��6   $��20(�69   $��SemiBold"�6G   
�������$��35,0,0,0q�6p   $ 0.5,0.5&�5#      5$      � 5%   
   $��0��6   $��0,40,0,0q�6   $��0��61   $��2��6>    � ColumnDefinitions 5&      �� 5'      $��70 �6#   �� 5(      $��* �6#   �� 5)      $��70 �6#    5+      �� 5,      $��* �6    �� 5-      $
��Auto �6    �� 5.      $
��Auto �6    �� 5/      $��1 �6    �� 50      $
��Auto �6    �� 51      $��* �6    .+�53      �� -   5         ��Name$ StatusLabel��53   ?   $	��0 5q�6   $��1��6"   $��1��62   $��Connecting...��6T   ���	#FF898888$��1q�54   +   $��13(�6?   � 55      -   5         �Name$ StatusProgressBar��55   a      FlatProgressBar#���   6   $��1��6D   $��2��6T   $��14��6|   ���	#FF40A6D1��6�   Z� 56      � 57      -   5        $ OveralStatusProgressBar��57   a   #���   6   $��1��6D   $��4��6T   $��14��6�   ���	#FF40A6D1��58      Z� 60   59      5:      5;      