   M S B A M L   `   `   `  ����K  GMonitor.TaskView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Monitor.TaskView.ViewMonitor.TaskView.View  5   	   .    (Monitor.TaskView.View.NotificationWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Monitor.TaskView.View     5   	     � Title$  NotificationWindow��5      $	��350��6#    � SizeToContent$ Height=�6/    � WindowStyle$
 None=�6F    � AllowsTransparency . 
������ $	��125��6�   .��5	      � -   5        ��5
      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ #eeeeee��6#    � Offset$ 0Q�63   � 5      $ #c3c3c3��6#   $ 1Q�63   � 5      $ #eeeeee��6#   $ 0.866Q�63   5       � RowDefinitions 5      �� 5      $
��Auto �6    $	��125��6.   .+�5      � $	��Top=�6   
���   �$��0��6?   $��30��6L   	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      
   
BlurRadius$
 10Q�6(      ShadowDepth$ 3Q�68      	Direction$	 270Q�6H      Opacity$	 0.2Q�6X    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      �� 5      $
��Auto �6'   .+�5      ]� $��2��6   $��0��60   $
��Left=�6@   ��.  $	��349��6q   .+�5       �� -   5         ��Name$ AVARTA��5       $��18��6,   $��18��68    K� ZIndex$ 1��6C   $
��Left=�6T   $��14,0,0,4q�6o   $��/Resource/net-on.png��6�   $��Uniform=�6�    J� RenderTransformOrigin$ 0.396,1.005&�6�   $��Bottom=�6�   �� 5"      $�� Alarm��6   $��Center=�6-   $��Bottom=�6O   
�������$��SemiBold"�5#      $��14(�6.    J� IsManipulationEnabled . $��0,5,5,5q�6Y   $��7,-1,0,0q�6k   $��31��6}   $	��100��6�   �� 5$      -   5         ��Name$ 
alarm_date��5$      $��
2017/12/28��60   $��Center=�6E   $��Bottom=�6g   
�������$��SemiBold"�5%      $��12(�6+    . $��0,5,5,5q�6V   $��7,-1,0,0q�6h   $��31��6z   $	��168��6�   �� 5&      -   5        $��35��5&      $	����6(      CloseMinMaxButtonStyle#���   64   
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�5(      $��20(�6O   $��Bottom=�5)      |�.  6W   5*      5+      � 5-      $��0��6   $��Stretch=�6#   $��Stretch=�6A   $ 0.5,0.5&�6]   .+�5.      �� -   5        $ lbl_message��5.      $��Label��6-   $
��Left=�6=   ���	#FF5F5F5F$��Center=�6p   $��Center=�6�   $	��310��6�   $ 0.5,0.5&�6�   $��Sitka Small)�6�   $��39��6�   $��
19,36,0,50q�6�   �� 5/      -   5        $ lbl_information��5/      $��Label��61   $
��Left=�6A   ���	#FF5F5F5F$��Center=�6t   $��Center=�6�   $	��310��6�   $ 0.5,0.5&�6�   $��Sitka Small)�6�   $��39��6�   $��
19,75,0,11q�6  $
��Bold"�6  50       � Triggers 51      :� 52       :� RoutedEvent$ FrameworkElement.Loaded��6   .[�53      �� -   5         ��Name$ StoryboardLoad��53   &   .��54      �� .��55      W�  �� TargetProperty$ (UIElement.Opacity)�6.    W� From$	 0.0X�6^   	 W� To$	 1.0X�6i    g� Duration$ 0:0:2D�6r   W� 56      -   5        $ (UIElement.Opacity)�56   .   $	 1.0X�6^   $	 0.0X�6i   $ 0:0:8D�6r    g� 	BeginTime$ 0:0:5X�6�   6�   57      58      59      :� 5;      $ Mouse.MouseEnter��6   [�5<      �� 5=       �� BeginStoryboardName$ StoryboardLoad��6+   �� 5>      $ StoryboardFade��6+   5@      :� 5B      $ Mouse.MouseLeave��6   .[�5C      �� -	   5        $ StoryboardFade��5C   &   .��5D      �� .��5E      W� -
   5        $ (UIElement.Opacity)�5E   .   $	 1.0X�6^   $	 0.0X�6i   $ 0:0:8D�6r   $ 0:0:2X�6�   6�   5F      5G      5H      5K      5N      